using Application.Dtos.Friend;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories.Communications;
using Infrastructure.EntityFramework.Repositories.Users;
using Infrastructure.EntityFramework.UnitOfWork;

namespace Application.UseCases.Friends;

public class UseCaseRejectFriendRequest: IUseCaseParameterizedQuery<bool, string, string>
{
    private readonly IFriendRepository _friendRepository;
    private readonly IFriendRequestRepository _friendRequestRepository;

    public UseCaseRejectFriendRequest(IFriendRepository friendRepository, IFriendRequestRepository friendRequestRepository)
    {
        _friendRepository = friendRepository;
        _friendRequestRepository = friendRequestRepository;
    }

    public bool Execute(string connectedUserId, string friendId)
    {
        if (_friendRepository.IsFriend(connectedUserId, friendId) || _friendRepository.IsFriend(friendId, connectedUserId))
            throw new Exception("Users are Already Friends");

        if (!_friendRequestRepository.IsRequestPresent(connectedUserId, friendId))
            throw new Exception("Request doesn't exists");

        var request = _friendRequestRepository.FetchRequestByIds(connectedUserId, friendId);
        var isDeleted = _friendRequestRepository.SetIsDone(request.Id, true);
        return isDeleted;
    }
}