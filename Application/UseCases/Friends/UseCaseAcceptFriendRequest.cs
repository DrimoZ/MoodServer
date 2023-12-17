using Application.Dtos.Friend;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories.Communications;
using Infrastructure.EntityFramework.Repositories.Users;
using Infrastructure.EntityFramework.UnitOfWork;

namespace Application.UseCases.Friends;

public class UseCaseAcceptFriendRequest: IUseCaseParameterizedWriter<DtoOutputFriend, string, string>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IFriendRepository _friendRepository;
    private readonly IFriendRequestRepository _friendRequestRepository;

    public UseCaseAcceptFriendRequest(IMapper mapper, IUnitOfWork unitOfWork, IFriendRepository friendRepository, IFriendRequestRepository friendRequestRepository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _friendRepository = friendRepository;
        _friendRequestRepository = friendRequestRepository;
    }

    public DtoOutputFriend Execute(string connectedUserId, string friendId)
    {
        if (_friendRepository.IsFriend(connectedUserId, friendId) || _friendRepository.IsFriend(friendId, connectedUserId))
            throw new Exception("Users are Already Friends");

        // The friend has emited the request => Swap Parameters
        if (!_friendRequestRepository.IsRequestPresent(friendId, connectedUserId))
            throw new Exception("Request doesn't exists");
        
        _unitOfWork.BeginTransaction();
        
        var dbFriend = new DbFriend
        {
            UserId = connectedUserId,
            FriendId = friendId
        };
        
        try
        {
            _friendRepository.Create(dbFriend);
            
            dbFriend.UserId = friendId;
            dbFriend.FriendId = connectedUserId;
            _friendRepository.Create(dbFriend);

            var request = _friendRequestRepository.FetchRequestByIds(connectedUserId, friendId);
            _friendRequestRepository.SetIsAccepted(request.Id, true);
            _friendRequestRepository.SetIsDone(request.Id, true);
        }
        catch (Exception e)
        {
            _unitOfWork.Rollback();
            throw new Exception(e.Message);
        }
        
        _unitOfWork.Commit();

        return _mapper.Map<DtoOutputFriend>(dbFriend);
    }
}