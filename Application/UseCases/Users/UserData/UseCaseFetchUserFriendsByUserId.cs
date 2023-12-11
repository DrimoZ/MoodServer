using Application.Dtos.User.UserData;
using Application.Dtos.User.UserProfile;
using Application.Services.Users;
using Application.Services.Users.Util;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories.Users;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Users.UserData;

public class UseCaseFetchUserFriendsByUserId: IUseCaseParameterizedQuery<DtoOutputUserFriends, string, string>
{
    private readonly ILogger<UseCaseFetchUserFriendsByUserId> _logger;
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly IFriendRepository _friendRepository;
    private readonly IMapper _mapper;

    public UseCaseFetchUserFriendsByUserId(IUserService userService, IMapper mapper, IUserRepository userRepository, IFriendRepository friendRepository, ILogger<UseCaseFetchUserFriendsByUserId> logger)
    {
        _userService = userService;
        _mapper = mapper;
        _userRepository = userRepository;
        _friendRepository = friendRepository;
        _logger = logger;
    }

    public DtoOutputUserFriends Execute(string connectedUser, string profileRequestUser)
    {
        var dbUser = _userRepository.FetchById(profileRequestUser);
        var isSameUser = connectedUser == profileRequestUser;
        
        var dto = new DtoOutputUserFriends
        {
            IsConnectedUser = isSameUser,
            IsFriendPublic = isSameUser || _friendRepository.IsFriend(connectedUser, profileRequestUser) || dbUser is { IsPublic: true, IsFriendPublic: true }
        };

        if (!dto.IsFriendPublic) return dto;

        var friends = _friendRepository
            .FetchFriends(profileRequestUser)
            .Select(dbU => _mapper.Map<DtoOutputUserFriends.DtoFriend>(dbU))
            .ToList();

        foreach (var friend in friends)
        {
            friend.IsFriendWithConnected = _friendRepository.IsFriend(connectedUser, friend.Id);
            friend.CommonFriendCount = friend.Id == connectedUser ? -1 : _friendRepository.FetchCommonFriendsCount(connectedUser, friend.Id);
        }

        dto.Friends = friends;
        return dto;
    }
}