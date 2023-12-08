using Application.Dtos.Friend;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Friends;

public class UseCaseDeleteFriend: IUseCaseParameterizedQuery<bool, string, string>
{
    private readonly IMapper _mapper;
    private readonly IFriendRepository _friendRepository;
    private readonly IUserRepository _userRepository;

    public UseCaseDeleteFriend(IMapper mapper, IFriendRepository friendRepository, IUserRepository userRepository)
    {
        _mapper = mapper;
        _friendRepository = friendRepository;
        _userRepository = userRepository;
    }


    public bool Execute(string userId, string friendId)
    {
        return _friendRepository.Delete(userId, friendId);
    }
}