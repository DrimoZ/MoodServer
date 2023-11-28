using Application.Dtos.Publication;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Publications;

public class UseCaseGetPublicationByUser
{
    private readonly IPublicationRepository _publicationRepository;
    private readonly IMapper _mapper;
    public UseCaseGetPublicationByUser(IPublicationRepository publicationRepository, IMapper mapper)
    {
        _publicationRepository = publicationRepository;
        _mapper = mapper;
    }

    public DtoOutputPublication Execute(DbUser user)
    {
        var dbPublication = _publicationRepository.FetchByUser(user);
        return _mapper.Map<DtoOutputPublication>(dbPublication);
    }
}