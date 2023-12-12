using Application.Dtos.User.UserData;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Users.UserData;

public class UseCaseGetAllUsers: IUseCaseParameterizedQuery<IEnumerable<DtoOutputUserDiscover>, string, int>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IFriendRepository _friendRepository;

    public UseCaseGetAllUsers(IMapper mapper, IUserRepository userRepository, IFriendRepository friendRepository)
    {
        _mapper = mapper;
        
        _userRepository = userRepository;
        _friendRepository = friendRepository;
    }

    public IEnumerable<DtoOutputUserDiscover> Execute(string connectedUserId, int requestCount)
    {
        var users = _userRepository
            .GetAll()
            .Where(user => user.Id != connectedUserId) 
            .Select(user => _mapper.Map<DtoOutputUserDiscover>(user))
            .ToList();

        foreach (var user in users)
        {
            user.IsFriendWithConnected = _friendRepository.IsFriend(connectedUserId, user.Id);
            user.CommonFriendCount = _friendRepository.FetchCommonFriendsCount(connectedUserId, user.Id);
        }
        
        return users;
    }
}