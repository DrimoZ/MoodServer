using Application.Dtos.Publication;
using Application.Services.Publications;
using Application.Services.Publications.Util;
using Application.UseCases.Utils;
using AutoMapper;
using Domain;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories.Accounts;
using Infrastructure.EntityFramework.Repositories.Publications;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Publications;

public class UseCaseGetFriendsPublications:IUseCaseParameterizedQuery<IEnumerable<DtoOutputPublication>, string, int>
{
    private readonly IMapper _mapper;
    
    private readonly IPublicationService _publicationService;
    private readonly IFriendRepository _friendRepository;
    private readonly IPublicationRepository _publicationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly ILikeRepository _likeRepository;

    public UseCaseGetFriendsPublications(IMapper mapper, IPublicationService publicationService, IFriendRepository friendRepository, IPublicationRepository publicationRepository, IUserRepository userRepository, IAccountRepository accountRepository, ILikeRepository likeRepository)
    {
        _mapper = mapper;
        _publicationService = publicationService;
        _friendRepository = friendRepository;
        _publicationRepository = publicationRepository;
        _userRepository = userRepository;
        _accountRepository = accountRepository;
        _likeRepository = likeRepository;
    }

    public IEnumerable<DtoOutputPublication> Execute(string connectedUserId, int pubCount)
    {
        var dbFriends = _friendRepository.FetchFriends(connectedUserId);
        var dbPublications = new List<DbPublication>();
        foreach (var dbFriend in dbFriends)
        {
            dbPublications.AddRange(_publicationRepository.FetchUserPublications(dbFriend.UserId));
        }

        var dbPublicationsByDate = dbPublications.OrderBy(p => p.PublicationDate).Reverse().ToList().Take(pubCount);

        var publications = new List<DtoOutputPublication>();
        foreach (var dbPublication in dbPublicationsByDate)
        {
            var publication = _publicationService.FetchPublicationById(dbPublication.PublicationId, 
                new [] { EPublicationFetchAttribute.Comments });
        
            var dtoPublication = _mapper.Map<DtoOutputPublication>(publication);

            dtoPublication.AuthorId = _publicationRepository.FetchById(dbPublication.PublicationId).UserId;

            var dbUser = _userRepository.FetchById(dtoPublication.AuthorId);
        
            dtoPublication.AuthorName = dbUser.UserName;
            dtoPublication.IsFromConnected = connectedUserId == dtoPublication.AuthorId;
            dtoPublication.AuthorImageId = _accountRepository.FetchById(dbUser.AccountId).ImageId;

            dtoPublication.HasConnectedLiked = _likeRepository.FetchLikeByUserAndPublication(connectedUserId, dbPublication.PublicationId) != null;
            
            publications.Add(dtoPublication);
        }

        return publications;
    }
}