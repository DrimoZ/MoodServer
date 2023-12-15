using Application.Dtos.UserGroup;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Groups;

public class UseCaseGetUserGroupByGroupIdUserId:IUseCaseParameterizedQuery<DtoOutputUserGroup, int, string>
{
    private readonly IUserGroupRepository _userGroupRepository;
    private readonly IMapper _mapper;

    public UseCaseGetUserGroupByGroupIdUserId(IUserGroupRepository userGroupRepository, IMapper mapper)
    {
        _userGroupRepository = userGroupRepository;
        _mapper = mapper;
    }

    public DtoOutputUserGroup Execute(int groupId, string userId)
    {
        return _mapper.Map<DtoOutputUserGroup>(_userGroupRepository.FetchByGroupIdUserId(groupId, userId));
    }
}