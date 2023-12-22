using Application.Dtos.Group;
using Application.UseCases.Utils;
using AutoMapper;
using Domain.Exception;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Repositories.Communications;
using Infrastructure.EntityFramework.Repositories.Users;
using Infrastructure.EntityFramework.UnitOfWork;

namespace Application.UseCases.Groups;

public class UseCaseGetGroupsByUserId:IUseCaseParameterizedQuery<IEnumerable<DtoOutputGroup>, string>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUserGroupRepository _userGroupRepository;
    private readonly IUserRepository _userRepository;
    private readonly IFriendRepository _friendRepository;
    private readonly IMapper _mapper;

    public UseCaseGetGroupsByUserId(IGroupRepository groupRepository, IMapper mapper, IUserGroupRepository userGroupRepository, IUserRepository userRepository, IFriendRepository friendRepository)
    {
        _groupRepository = groupRepository;
        _mapper = mapper;
        _userGroupRepository = userGroupRepository;
        _userRepository = userRepository;
        _friendRepository = friendRepository;
    }

    public IEnumerable<DtoOutputGroup> Execute(string userId)
    {
        List<DtoOutputGroup> grps = new List<DtoOutputGroup>();
        try
        {
            var userGroups = _userGroupRepository.FetchAllByUserId(userId);
            foreach (var usergrp in userGroups)
            {
                if (usergrp.HasLeft) continue;
                var grp = _mapper.Map<DtoOutputGroup>(_groupRepository.FetchById(usergrp.GroupId));
                if (grp.IsPrivate)
                {
                    var userInGroups = _userGroupRepository.FetchAllByGroupId(grp.Id).ToList();
                    if (!_friendRepository.IsFriend(userInGroups[0].UserId, userInGroups[1].UserId)) continue;
                }
                if (grp.Name == null)
                {
                    var userInGroups = _userGroupRepository.FetchAllByGroupId(grp.Id).ToList();
                    
                    if (userInGroups.Count() < 3)
                    {
                        
                        foreach(var  userGroup in userInGroups)
                        {
                            if(userGroup.UserId != userId)
                            {
                                try
                                {
                                    grp.Name = _userRepository.FetchById(userGroup.UserId).UserName;
                                    break;
                                }
                                catch (DeletedUserException e)
                                {
                                    grp.Name = "Deleted";
                                    break;
                                    Console.WriteLine(e);
                                }
                                
                            }
                        }
                    }
                    else
                    {
                        grp.Name = "Untitled";
                    }
                    
                }
                if(grp.Name == "Deleted") continue;
                grps.Add(grp);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return grps;
    }
}
