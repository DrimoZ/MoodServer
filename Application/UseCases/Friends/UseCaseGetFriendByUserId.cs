using Application.Dtos.Friend;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Friends;

public class UseCaseGetFriendByUserId:IUseCaseParameterizedQuery<DtoOutputFriend, string, string>
{
    private readonly IMapper _mapper;
    private readonly IFriendRepository _friendRepository;

    public UseCaseGetFriendByUserId(IMapper mapper, IFriendRepository friendRepository)
    {
        _mapper = mapper;
        _friendRepository = friendRepository;
    }


    public DtoOutputFriend Execute(string connectedUserId, string profileRequestUserId)
    {
        DbFriend friend = new DbFriend
        {
            UserId = connectedUserId,
            FriendId = profileRequestUserId
        };
        var entity = _friendRepository.Create(friend);

        return _mapper.Map<DtoOutputFriend>(friend);
    }
}