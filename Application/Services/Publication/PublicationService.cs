using Application.Services.Publication.Util;
using AutoMapper;
using Infrastructure.EntityFramework.DbComplexEntities;
using Infrastructure.EntityFramework.Repositories.Publications;

namespace Application.Services.Publication;

public class PublicationService: IPublicationService
{
    private readonly IMapper _mapper;
    
    private readonly IPublicationRepository _publicationRepository;
    private readonly IPublicationElementRepository _elementRepository;
    private readonly ILikeRepository _likeRepository;
    private readonly ICommentRepository _commentRepository;

    public PublicationService(IMapper mapper, IPublicationRepository publicationRepository, IPublicationElementRepository elementRepository, ICommentRepository commentRepository, ILikeRepository likeRepository)
    {
        _mapper = mapper;
        _publicationRepository = publicationRepository;
        _elementRepository = elementRepository;
        _commentRepository = commentRepository;
        _likeRepository = likeRepository;
    }
    
    public IEnumerable<Domain.Publication> FetchPublicationsByUserId(string userId)
    {
        return _publicationRepository
            .FetchUserPublications(userId)
            .Select(p => FetchPublicationById(p.Id, Array.Empty<EPublicationFetchAttribute>()));
    }

    public Domain.Publication FetchPublicationById(int id, IEnumerable<EPublicationFetchAttribute> attributesToFetch)
    {
        var dbComplex = FetchComplexByPublicationId(id);
        var publication = _mapper.Map<Domain.Publication>(dbComplex);

        publication.LikeCount = _likeRepository.FetchLikeCountByPublicationId(id);
        publication.CommentCount = _commentRepository.FetchCommentCountByPublicationId(id);
        
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
        return publication;
    }

    private DbComplexPublication FetchComplexByPublicationId(int pubId)
    {
        var complexPublication = _mapper.Map<DbComplexPublication>(_publicationRepository.FetchById(pubId));
        complexPublication.Elements = _elementRepository.FetchElementsByPublicationId(pubId);
        
        return complexPublication;
    }
}