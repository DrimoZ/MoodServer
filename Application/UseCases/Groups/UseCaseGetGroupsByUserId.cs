using Application.Dtos.Group;
using Application.UseCases.Utils;
using AutoMapper;
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
    private readonly IMapper _mapper;

    public UseCaseGetGroupsByUserId(IGroupRepository groupRepository, IMapper mapper, IUserGroupRepository userGroupRepository, IUserRepository userRepository)
    {
        _groupRepository = groupRepository;
        _mapper = mapper;
        _userGroupRepository = userGroupRepository;
        _userRepository = userRepository;
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
                if (grp.Name == null)
                {
                    var usergroups = _userGroupRepository.FetchAllByGroupId(grp.Id).ToList();
                    
                    if (usergroups.Count() < 3)
                    {
                        foreach(var  userGroup in usergroups)
                        {
                            if (userGroup.UserId != userId)
                            {
                                grp.Name = _userRepository.FetchById(userGroup.UserId).Name;
                                break;
                            }
                        }
                    }
                    else
                    {
                        grp.Name = "Untitled";
                    }
                    
                }
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
