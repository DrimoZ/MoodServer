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
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IFriendRepository _friendRepository;
    private readonly IFriendRequestRepository _friendRequestRepository;

    public UseCaseRejectFriendRequest(IMapper mapper, IFriendRepository friendRepository, IFriendRequestRepository friendRequestRepository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _friendRepository = friendRepository;
        _friendRequestRepository = friendRequestRepository;
        _unitOfWork = unitOfWork;
    }

    public bool Execute(string connectedUserId, string friendId)
    {
        if (_friendRepository.IsFriend(connectedUserId, friendId) || _friendRepository.IsFriend(friendId, connectedUserId))
            throw new Exception("Users are Already Friends");

        if (!_friendRequestRepository.IsRequestPresent(friendId, connectedUserId))
            throw new Exception("Request doesn't exists");
        
        _unitOfWork.BeginTransaction();
        var request = _friendRequestRepository.FetchRequestByIds(friendId, connectedUserId);
        var isDeleted = _friendRequestRepository.Delete(request.Id);
        _unitOfWork.Commit();
        return isDeleted;
    }
}