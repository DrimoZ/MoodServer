using Application.Dtos.Friend;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories;

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


    public DtoOutputFriend Execute(string userId, string otherId)
    {
        DbFriend friend = new DbFriend
        {
            UserId = userId,
            FriendId = otherId
        };
        var entity = _friendRepository.Create(friend);

        return _mapper.Map<DtoOutputFriend>(friend);
    }
}