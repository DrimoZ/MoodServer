using Infrastructure.EntityFramework.Repositories.Communications;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.Services.Users;

public class FriendService: IFriendService
{
    private readonly IFriendRepository _friendRepository;
    private readonly IFriendRequestRepository _friendRequestRepository;

    public FriendService(IFriendRepository friendRepository, IFriendRequestRepository friendRequestRepository)
    {
        _friendRepository = friendRepository;
        _friendRequestRepository = friendRequestRepository;
    }


    public int GetFriendStatus(string connectedUserId, string userToFetch)
    {
        if (_friendRepository.IsFriend(connectedUserId, userToFetch)) return 2;
        if (_friendRequestRepository.IsRequestPresent(userToFetch, connectedUserId)) return 1;
        if (_friendRequestRepository.IsRequestPresent(connectedUserId, userToFetch)) return 0;
        return -1;
    }
}