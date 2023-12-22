using Application.Dtos.User.UserProfile;
using Application.Services.Users;
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
    private readonly IAccountRepository _accountRepository;
    private readonly IFriendService _friendService;

    public UseCaseFetchUserFriendsByUserId(IMapper mapper, IUserRepository userRepository, IFriendRepository friendRepository, IAccountRepository accountRepository, IFriendService friendService)
    {
        _mapper = mapper;
        
        _userRepository = userRepository;
        _friendRepository = friendRepository;
        _accountRepository = accountRepository;
        _friendService = friendService;
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
                dtoUser.ImageId = acc.ImageId;
                return dtoUser;
            })
            .ToList();

        foreach (var friend in friends)
        {
            friend.IsFriendWithConnected = _friendService.GetFriendStatus(connectedUserId, friend.UserId);
            friend.CommonFriendCount = friend.UserId == connectedUserId ? -1 : _friendRepository.FetchCommonFriendsCount(connectedUserId, friend.UserId) - 1;
        }

        dto.Friends = friends;
        return dto;
    }
}
