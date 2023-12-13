using Application.Dtos.User.UserData;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Repositories.Communications;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Users.UserData;

public class UseCaseGetAllUsers: IUseCaseParameterizedQuery<IEnumerable<DtoOutputUserDiscover>, string, int>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IFriendRepository _friendRepository;
    private readonly IFriendRequestRepository _friendRequestRepository;

    public UseCaseGetAllUsers(IMapper mapper, IUserRepository userRepository, IFriendRepository friendRepository, IFriendRequestRepository friendRequestRepository)
    {
        _mapper = mapper;
        
        _userRepository = userRepository;
        _friendRepository = friendRepository;
        _friendRequestRepository = friendRequestRepository;
    }

    public IEnumerable<DtoOutputUserDiscover> Execute(string connectedUserId, int profileRequestUserId)
    {
        var users = _userRepository
            .GetAll()
            .Where(user => user.Id != connectedUserId) 
            .Select(user => _mapper.Map<DtoOutputUserDiscover>(user))
            .ToList();

        foreach (var user in users)
        {
            if (_friendRepository.IsFriend(connectedUserId, user.Id))
            {
                user.IsFriendWithConnected = 2;
            }
            else if (_friendRequestRepository.IsRequestPresent(user.Id, connectedUserId))
            {
                user.IsFriendWithConnected = 1;
            }
            else if (_friendRequestRepository.IsRequestPresent(connectedUserId, user.Id))
            {
                user.IsFriendWithConnected = 0;
            }
            else
            {
                user.IsFriendWithConnected = -1;
            }
            
            
            user.CommonFriendCount = _friendRepository.FetchCommonFriendsCount(connectedUserId, user.Id);
        }
        
        return users;
    }
}