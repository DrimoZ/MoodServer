using Application.Dtos.Publication;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Publications;

public class UseCaseGetPublicationByUser:IUseCaseParameterizedQuery<List<DtoOutputPublication>, int>
{
    private readonly IPublicationRepository _publicationRepository;
    private readonly IMapper _mapper;
    public UseCaseGetPublicationByUser(IPublicationRepository publicationRepository, IMapper mapper)
    {
        _publicationRepository = publicationRepository;
        _mapper = mapper;
    }

    public List<DtoOutputPublication> Execute(int userId)
    {
            
    }
}