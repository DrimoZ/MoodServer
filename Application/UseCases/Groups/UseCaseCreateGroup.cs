using Application.Dtos.Group;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Groups;

public class UseCaseCreateGroup:IUseCaseParameterizedWriter<DbGroup, DtoInputCreateGroup, IEnumerable<string>>
{
    private readonly IGroupRepository _groupRepository;
    private IMapper _mapper;
    public UseCaseCreateGroup(IGroupRepository groupRepository, IMapper mapper)
    {
        _groupRepository = groupRepository;
        _mapper = mapper;
    }

    public DbGroup Execute(DtoInputCreateGroup input, IEnumerable<string> userIds)
    {
        var dbAccount = _groupRepository.Create(_mapper.Map<DbGroup>(input), userIds);
        return dbAccount;
    }
}