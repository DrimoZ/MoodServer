using Application.Dtos.User.UserProfile;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories.Accounts;
using Infrastructure.EntityFramework.Repositories.Communications;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Users.User;

public class UseCaseFetchUserFriendsByUserId: IUseCaseParameterizedQuery<DtoOutputUserFriends, string, string>
{
    private readonly IMapper _mapper;
    
    private readonly IUserRepository _userRepository;
    private readonly IFriendRepository _friendRepository;
    private readonly IFriendRequestRepository _friendRequestRepository;
    private readonly IAccountRepository _accountRepository;

    public UseCaseFetchUserFriendsByUserId(IMapper mapper, IUserRepository userRepository, IFriendRepository friendRepository, IFriendRequestRepository friendRequestRepository, IAccountRepository accountRepository)
    {
        _mapper = mapper;
        
        _userRepository = userRepository;
        _friendRepository = friendRepository;
        _friendRequestRepository = friendRequestRepository;
        _accountRepository = accountRepository;
    }

    public DtoOutputUserFriends Execute(string connectedUserId, string profileRequestUserId)
    {
        var dbUser = _userRepository.FetchById(profileRequestUserId);
        var isSameUser = connectedUserId == profileRequestUserId;
        
        var dto = new DtoOutputUserFriends
        {
            IsConnectedUser = isSameUser,
            IsFriendPublic = isSameUser || _friendRepository.IsFriend(connectedUserId, profileRequestUserId) || dbUser is { IsPublic: true, IsFriendPublic: true }
        };

        if (!dto.IsFriendPublic) return dto;

        var friends = _friendRepository
            .FetchFriends(profileRequestUserId)
            .Select(dbU => {
                var acc = _accountRepository.FetchById(dbU.AccountId);
                var dtoUser =  _mapper.Map<DtoOutputUserFriends.DtoFriend>(dbU);
                dtoUser.IdImage = acc.ImageId;
                return dtoUser;
            })
            .ToList();

        foreach (var friend in friends)
        {
            if (_friendRepository.IsFriend(connectedUserId, friend.Id))
            {
                friend.IsFriendWithConnected = 2;
            }
            else if (_friendRequestRepository.IsRequestPresent(friend.Id, connectedUserId))
            {
                friend.IsFriendWithConnected = 1;
            }
            else if (_friendRequestRepository.IsRequestPresent(connectedUserId, friend.Id))
            {
                friend.IsFriendWithConnected = 0;
            }
            else
            {
                friend.IsFriendWithConnected = -1;
            }
            friend.CommonFriendCount = friend.Id == connectedUserId ? -1 : _friendRepository.FetchCommonFriendsCount(connectedUserId, friend.Id);
        }

        dto.Friends = friends;
        return dto;
    }
}