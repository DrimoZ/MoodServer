using Application.Dtos.Group;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.UnitOfWork;

namespace Application.UseCases.Groups;

public class UseCaseCreateGroup:IUseCaseParameterizedWriter<DbGroup, DtoInputCreateGroup, IEnumerable<string>>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUserGroupRepository _userGroupRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public UseCaseCreateGroup(IGroupRepository groupRepository, IMapper mapper, IUnitOfWork unitOfWork, IUserGroupRepository userGroupRepository, IUserRepository userRepository)
    {
        _groupRepository = groupRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userGroupRepository = userGroupRepository;
        _userRepository = userRepository;
    }

    public DbGroup Execute(DtoInputCreateGroup input, IEnumerable<string> userIds)
    {
        _unitOfWork.BeginTransaction();

        var group = _mapper.Map<DbGroup>(input);
        _groupRepository.Create(group);
        _unitOfWork.Save();
        
        foreach (var userId in userIds)
        {
            try
            {
                _userRepository.FetchById(userId);
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                Console.WriteLine(e);
                throw;
            }
            
            var usrgrp= new DbUserGroup
            {
                UserId = userId,
                GroupId = group.Id
            };
            _userGroupRepository.Create(usrgrp);

        }
        _unitOfWork.Save();
        _unitOfWork.Commit();
        return group;
    }
}