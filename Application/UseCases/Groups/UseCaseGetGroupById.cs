using Application.Dtos.Group;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories.Communications;

namespace Application.UseCases.Groups;

public class UseCaseGetGroupById:IUseCaseParameterizedQuery<DtoOutputGroup, int>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IMapper _mapper;
    
    public UseCaseGetGroupById(IGroupRepository groupRepository, IMapper mapper)
    {
        _groupRepository = groupRepository;
        _mapper = mapper;
    }

    public DtoOutputGroup Execute(int id)
    {
        return _mapper.Map<DtoOutputGroup>(_groupRepository.FetchById(id));
    }
}