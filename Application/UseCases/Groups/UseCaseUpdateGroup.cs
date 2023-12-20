using Application.Dtos.Group;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories.Communications;

namespace Application.UseCases.Groups;

public class UseCaseUpdateGroup: IUseCaseWriter<bool, DtoInputUpdateGroup>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IMapper _mapper;

    public UseCaseUpdateGroup(IGroupRepository groupRepository, IMapper mapper)
    {
        _groupRepository = groupRepository;
        _mapper = mapper;
    }

    public bool Execute(DtoInputUpdateGroup input)
    {
        return _groupRepository.UpdateGroup(_mapper.Map<DbGroup>(input));
    }
}