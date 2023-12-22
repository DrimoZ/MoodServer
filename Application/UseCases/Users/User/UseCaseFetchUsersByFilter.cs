using Application.Dtos.User.User;
using Application.Services.Users;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories.Accounts;
using Infrastructure.EntityFramework.Repositories.Communications;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Users.User;

public class UseCaseFetchUsersByFilter: IUseCaseParameterizedQuery<IEnumerable<DtoOutputDiscoverUser>, string, int, string>
{
    private readonly IMapper _mapper;
    
    private readonly IUserRepository _userRepository;
    private readonly IFriendRepository _friendRepository;
    private readonly IFriendRequestRepository _friendRequestRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IFriendService _friendService;
    
    public UseCaseFetchUsersByFilter(IMapper mapper, IUserRepository userRepository, IFriendRepository friendRepository, IFriendRequestRepository friendRequestRepository, IAccountRepository accountRepository, IFriendService friendService)
    {
        _mapper = mapper;
        
        _userRepository = userRepository;
        _friendRepository = friendRepository;
        _friendRequestRepository = friendRequestRepository;
        _accountRepository = accountRepository;
        _friendService = friendService;
    }

    public IEnumerable<DtoOutputDiscoverUser> Execute(string connectedUserId, int userCount, string searchValue)
    {
        var users = _userRepository
            .FetchUsersByFilter(connectedUserId, searchValue, userCount)
            .Select(user => {
                var acc = _accountRepository.FetchById(user.AccountId);
                var dtoUser = _mapper.Map<DtoOutputDiscoverUser>(user);
                dtoUser.ImageId = acc.ImageId;
                return dtoUser;
            })
            .ToList();

        foreach (var user in users)
        {
            user.IsFriendWithConnected = _friendService.GetFriendStatus(connectedUserId, user.UserId);
            user.CommonFriendCount = _friendRepository.FetchCommonFriendsCount(connectedUserId, user.UserId);
        }
        
        return users;
    }
}