using Application.Services.Users.Util;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories;

namespace Application.Services.Publication;

public class PublicationService: IPublicationService
{
    private readonly IPublicationRepository _publicationRepository;
    private readonly IMapper _mapper;

    public PublicationService(IPublicationRepository publicationRepository, IMapper mapper)
    {
        _publicationRepository = publicationRepository;
        _mapper = mapper;
    }

    public Domain.Publication FetchById(int id, IEnumerable<EPublicationFetchAttribute> attributesToFetch)
    {
        var dbPublication = _publicationRepository.FetchById(id);
        var user = _mapper.Map<Domain.Publication>(dbPublication);

        foreach (var attribute in attributesToFetch)
        {
            switch (attribute)
            {
                case EPublicationFetchAttribute.Comments:
                    
                    break;
                case EPublicationFetchAttribute.Likes:
                    
                    break;
                default:
                    throw new ArgumentException($"Unknown attribute: {attribute}");
            }
        }
        return user;
    }
}