using Application.Dtos.Publication;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Publications;

public class UseCaseGetPublicationById
{
    private readonly IPublicationRepository _publicationRepository;
    private readonly IMapper _mapper;
    public UseCaseGetPublicationById(IPublicationRepository publicationRepository, IMapper mapper)
    {
        _publicationRepository = publicationRepository;
        _mapper = mapper;
    }
}