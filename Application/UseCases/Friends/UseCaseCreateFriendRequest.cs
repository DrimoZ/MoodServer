using Application.Dtos.Friend;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Repositories.Communications;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Friends;

public class UseCaseCreateFriendRequest: IUseCaseParameterizedWriter<DtoOutputFriendRequest, string, string>
{
    private readonly IMapper _mapper;

    private readonly IFriendRequestRepository _friendRequestRepository;
    private readonly IFriendRepository _friendRepository;

    public UseCaseCreateFriendRequest(IMapper mapper, IFriendRequestRepository friendRequestRepository, IFriendRepository friendRepository)
    {
        _mapper = mapper;
        _friendRequestRepository = friendRequestRepository;
        _friendRepository = friendRepository;
    }


    public DtoOutputFriendRequest Execute(string connectedUserId, string friendId)
    {
        if (_friendRepository.IsFriend(connectedUserId, friendId))
            throw new Exception("Users are Already Friends");

        if (_friendRequestRepository.IsRequestPresent(connectedUserId, friendId))
            throw new Exception("Request already exists");
        
        var request = new DbFriendRequest
        {
            UserId = connectedUserId,
            FriendId = friendId
        };
        
        var entity = _friendRequestRepository.Create(request);

        return _mapper.Map<DtoOutputFriendRequest>(entity);
    }
}