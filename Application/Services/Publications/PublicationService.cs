using Application.Services.Publication;
using Application.Services.Publications.Util;
using AutoMapper;
using Domain;
using Infrastructure.EntityFramework.DbComplexEntities;
using Infrastructure.EntityFramework.Repositories.Accounts;
using Infrastructure.EntityFramework.Repositories.Publications;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.Services.Publications;

public class PublicationService: IPublicationService
{
    private readonly IMapper _mapper;
    
    private readonly IPublicationRepository _publicationRepository;
    private readonly IPublicationElementRepository _elementRepository;
    private readonly ILikeRepository _likeRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;

    public PublicationService(IMapper mapper, IPublicationRepository publicationRepository, IPublicationElementRepository elementRepository, ICommentRepository commentRepository, ILikeRepository likeRepository, IUserRepository userRepository, IAccountRepository accountRepository)
    {
        _mapper = mapper;
        _publicationRepository = publicationRepository;
        _elementRepository = elementRepository;
        _commentRepository = commentRepository;
        _likeRepository = likeRepository;
        _userRepository = userRepository;
        _accountRepository = accountRepository;
    }
    
    public IEnumerable<Domain.Publication> FetchPublicationsByUserId(string userId)
    {
        return _publicationRepository
            .FetchUserPublications(userId)
            .Select(p => FetchPublicationById(p.Id, Array.Empty<EPublicationFetchAttribute>()));
    }
    
    public IEnumerable<Domain.Publication> FetchPublicationsWithoutUserId(string userId, string searchValue)
    {
        return _publicationRepository
            .FetchPublicationsByFilter(userId)
            .Where(publication => _userRepository.FetchById(publication.UserId).Name.ToLower().Contains(searchValue))
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
                    var dbComments = _commentRepository.FetchCommentsByPublicationId(id);
                    var comments = dbComments.Select(dbComment => _mapper.Map<Comment>(dbComment)).ToList();

                    foreach (var comment in comments)
                    {
                        var dbUser = _userRepository.FetchById(comment.IdAuthor);
                        var dbAccount = _accountRepository.FetchById(dbUser.AccountId);

                        comment.NameAuthor = dbUser.Name;
                        comment.IdAuthorImage = dbAccount.ImageId;
                    }
                    
                    publication.AddRange(comments);
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