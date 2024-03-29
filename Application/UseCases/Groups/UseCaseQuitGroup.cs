using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories.Communications;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Groups;

public class UseCaseQuitGroup:IUseCaseParameterizedQuery<bool, int, string>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUserGroupRepository _userGroupRepository;

    public UseCaseQuitGroup(IGroupRepository groupRepository, IUserGroupRepository userGroupRepository)
    {
        _groupRepository = groupRepository;
        _userGroupRepository = userGroupRepository;
    }

    public bool Execute(int groupId, string userId)
    {
        var userGroup = _userGroupRepository.FetchByGroupIdUserId(groupId, userId);
        var group = _groupRepository.FetchById(userGroup.GroupId);
        
        var usergroups = _userGroupRepository.FetchAllByGroupId(group.Id);
        
        if (group.IsPrivate)
        {
            foreach (var usergroup in usergroups)
            {
                _userGroupRepository.ToggleUserQuitGroup(userGroup);
            }

            return true;
        }
        if(userGroup.HasLeft == false) _userGroupRepository.ToggleUserQuitGroup(userGroup);
        
        if (usergroups.All(grp => grp.HasLeft == true)) _groupRepository.Delete(group);
        
        else if(userGroup.UserId == group.ProprioId)
        {
            var userGroups = _userGroupRepository.FetchAllByGroupId(group.Id);
            foreach (var dbUserGroup in userGroups)
            {
                if (dbUserGroup.UserId != group.ProprioId )
                {
                    group.ProprioId = dbUserGroup.UserId;
                    _groupRepository.UpdateGroup(group);
                    break;
                }
            }
        }
        return true;
    }
}