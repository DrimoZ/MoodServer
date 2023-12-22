using Application.Dtos.Publication;
using Application.Dtos.User;
using Application.Services.Publications;
using Application.Services.Publications.Util;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Repositories.Accounts;
using Infrastructure.EntityFramework.Repositories.Publications;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Publications;

public class UseCaseGetPublicationById:IUseCaseParameterizedQuery<DtoOutputPublication, string, int>
{
    private readonly IPublicationRepository _publicationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IPublicationService _publicationService;
    private readonly ILikeRepository _likeRepository;
    private readonly IMapper _mapper;

    public UseCaseGetPublicationById(IPublicationRepository publicationRepository, IMapper mapper, IPublicationService publicationService, IUserRepository userRepository, IAccountRepository accountRepository, ILikeRepository likeRepository)
    {
        _publicationRepository = publicationRepository;
        _mapper = mapper;
        _publicationService = publicationService;
        _userRepository = userRepository;
        _accountRepository = accountRepository;
        _likeRepository = likeRepository;
    }

    public DtoOutputPublication Execute(string connectedUserId, int pubId)
    {
        try
        {
            var publication = _publicationService.FetchPublicationById(pubId,
                new[] { EPublicationFetchAttribute.Comments });

            var dtoPublication = _mapper.Map<DtoOutputPublication>(publication);

            dtoPublication.AuthorId = _publicationRepository.FetchById(pubId).UserId;

            var dbUser = _userRepository.FetchById(dtoPublication.AuthorId);

            dtoPublication.AuthorRole = dbUser.UserRole;
            dtoPublication.AuthorName = dbUser.UserName;
            dtoPublication.IsFromConnected = connectedUserId == dtoPublication.AuthorId;
            dtoPublication.AuthorImageId = _accountRepository.FetchById(dbUser.AccountId).ImageId;

            dtoPublication.HasConnectedLiked =
                _likeRepository.FetchLikeByUserAndPublication(connectedUserId, pubId) != null;

            return dtoPublication;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}