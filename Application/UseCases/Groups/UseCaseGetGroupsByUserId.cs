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
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UseCaseGetGroupsByUserId(IGroupRepository groupRepository, IMapper mapper, IUnitOfWork unitOfWork, IUserGroupRepository userGroupRepository)
    {
        _groupRepository = groupRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userGroupRepository = userGroupRepository;
    }

    public IEnumerable<DtoOutputGroup> Execute(string userId)
    {
        List<DtoOutputGroup> grps = new List<DtoOutputGroup>();
        try
        {
            var userGroups = _userGroupRepository.FetchAllByUserId(userId);
            foreach (var usergrp in userGroups)
            {
                var grp = _mapper.Map<DtoOutputGroup>(_groupRepository.FetchById(usergrp.GroupId));
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
