using Application.Dtos.User.UserProfile;
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
    private readonly IFriendRequestRepository _friendRequestRepository;

    public UseCaseFetchUserProfileByUserId(IMapper mapper, IUserRepository userRepository, IFriendRepository friendRepository, IPublicationRepository publicationRepository, IAccountRepository accountRepository, IFriendRequestRepository friendRequestRepository)
    {
        _mapper = mapper;
        
        _userRepository = userRepository;
        _friendRepository = friendRepository;
        _publicationRepository = publicationRepository;
        _accountRepository = accountRepository;
        _friendRequestRepository = friendRequestRepository;
    }

    public DtoOutputUserProfile Execute(string connectedUserId, string profileRequestUserId)
    {
        var dbUser = _userRepository.FetchById(profileRequestUserId);
        var dbAccount = _accountRepository.FetchById(dbUser.AccountId);
        
        var user = _mapper.Map<DtoOutputUserProfile>(dbUser);
        
        if (_friendRepository.IsFriend(connectedUserId, dbUser.Id))
        {
            user.IsFriendWithConnected = 2;
        }
        else if (_friendRequestRepository.IsRequestPresent(dbUser.Id, connectedUserId))
        {
            user.IsFriendWithConnected = 1;
        }
        else if (_friendRequestRepository.IsRequestPresent(connectedUserId, dbUser.Id))
        {
            user.IsFriendWithConnected = 0;
        }
        else
        {
            user.IsFriendWithConnected = -1;
        }
        
        user.IdImage = dbAccount.ImageId;
        user.IsConnectedUser = connectedUserId == profileRequestUserId;
        user.FriendCount = _friendRepository.FetchFriendCount(profileRequestUserId);
        user.PublicationCount = _publicationRepository.FetchPublicationCount(profileRequestUserId);
        user.Description = dbAccount.Description!;

        return user;
    }
}