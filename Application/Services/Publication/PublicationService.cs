using AutoMapper;
using Domain;
using Infrastructure.EntityFramework.Repositories;

namespace Application.Services.Users.Util;

public class PublicationService:IPublicationService
{
    private readonly IPublicationRepository _publicationRepository;
    private readonly IMapper _mapper;

    public PublicationService(IPublicationRepository publicationRepository, IMapper mapper)
    {
        _publicationRepository = publicationRepository;
        _mapper = mapper;
    }

    public Publication FetchById(string id, IEnumerable<EPublicationFetchAttribute> attributesToFetch)
    {
        var dbPublication = _publicationRepository.FetchById(id);
        var user = _mapper.Map<Publication>(dbPublication);

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