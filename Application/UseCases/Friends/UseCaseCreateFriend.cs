using Application.Dtos.Friend;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Friends;

public class UseCaseCreateFriend:IUseCaseParameterizedWriter<DtoOutputFriend, string, string>
{
    private readonly IMapper _mapper;
    private readonly IFriendRepository _friendRepository;
    private readonly IUserRepository _userRepository;
    public UseCaseCreateFriend(IMapper mapper, IFriendRepository friendRepository, IUserRepository userRepository)
    {
        _mapper = mapper;
        _friendRepository = friendRepository;
        _userRepository = userRepository;
    }
    public DtoOutputFriend Execute(string userId, string loginFriend)
    {
        var user = _userRepository.FetchByLogin(loginFriend);
        var friend = new DbFriend
        {
            UserId = userId,
            FriendId = user.Id
        };
        
        var entity = _friendRepository.Create(friend);
        friend.UserId = user.Id;
        friend.FriendId = userId;
        _friendRepository.Create(friend);
        return _mapper.Map<DtoOutputFriend>(friend);
    }
}