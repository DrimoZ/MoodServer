using Application.Dtos.User.UserProfile;
using Application.Services.Users;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories.Accounts;
using Infrastructure.EntityFramework.Repositories.Communications;
using Infrastructure.EntityFramework.Repositories.Publications;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Users.User;

public class UseCaseFetchUserProfileByUserId: IUseCaseParameterizedQuery<DtoOutputUserProfile, string, string>
{
    private readonly IMapper _mapper;
    
    private readonly IUserRepository _userRepository;
    private readonly IFriendRepository _friendRepository;
    private readonly IPublicationRepository _publicationRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IFriendService _friendService;

    public UseCaseFetchUserProfileByUserId(IMapper mapper, IUserRepository userRepository, IFriendRepository friendRepository, IPublicationRepository publicationRepository, IAccountRepository accountRepository, IFriendService friendService)
    {
        _mapper = mapper;
        
        _userRepository = userRepository;
        _friendRepository = friendRepository;
        _publicationRepository = publicationRepository;
        _accountRepository = accountRepository;
        _friendService = friendService;
    }

    public DtoOutputUserProfile Execute(string connectedUserId, string profileRequestUserId)
    {
        var dbUser = _userRepository.FetchById(profileRequestUserId);
        var dbAccount = _accountRepository.FetchById(dbUser.AccountId);
        
        var user = _mapper.Map<DtoOutputUserProfile>(dbUser);

        user.IsFriendWithConnected = _friendService.GetFriendStatus(connectedUserId, dbUser.UserId);
        user.IsConnectedUser = connectedUserId == profileRequestUserId;
        
        user.ImageId = dbAccount.ImageId;
        user.AccountDescription = dbAccount.AccountDescription!;

        user.FriendCount = _friendRepository.FetchFriendCount(profileRequestUserId);
        user.PublicationCount = _publicationRepository.FetchPublicationCount(profileRequestUserId);
        
        return user;
    }
}