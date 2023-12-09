using Application.Dtos.Publication;
using Application.Services.Publication;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Repositories.Publications;

namespace Application.UseCases.Publications;

public class UseCaseGetPublicationByUser:IUseCaseParameterizedQuery<List<DtoOutputPublication>, string>
{
    private readonly IMapper _mapper;
    
    private readonly IPublicationService _publicationService;

    public UseCaseGetPublicationByUser(IMapper mapper, IPublicationService publicationService)
    {
        _mapper = mapper;
        _publicationService = publicationService;
    }

    public List<DtoOutputPublication> Execute(string userId)
    {
        return _publicationService.FetchPublicationsByUserId(userId).Select(p => _mapper.Map<DtoOutputPublication>(p)).ToList();
    }
}