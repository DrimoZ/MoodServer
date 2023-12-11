using Application.Dtos.Publication;
using Application.Dtos.User;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Repositories.Publications;

namespace Application.UseCases.Publications;

public class UseCaseGetPublicationById:IUseCaseParameterizedQuery<DtoOutputPublication, int>
{
    private readonly IPublicationRepository _publicationRepository;
    private readonly IMapper _mapper;

    public UseCaseGetPublicationById(IPublicationRepository publicationRepository, IMapper mapper)
    {
        _publicationRepository = publicationRepository;
        _mapper = mapper;
    }

    public DtoOutputPublication Execute(int id)
    {
        var dbPublication = _publicationRepository.FetchById(id);
        return _mapper.Map<DtoOutputPublication>(dbPublication);
    }
}