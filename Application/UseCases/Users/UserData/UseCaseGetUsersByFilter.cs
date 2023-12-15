using Application.Dtos.User.UserData;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Repositories.Accounts;
using Infrastructure.EntityFramework.Repositories.Communications;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Users.UserData;

public class UseCaseGetUsersByFilter: IUseCaseParameterizedQuery<IEnumerable<DtoOutputUserDiscover>, string, int, string>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IFriendRepository _friendRepository;
    private readonly IFriendRequestRepository _friendRequestRepository;
    private readonly IAccountRepository _accountRepository;
    
    public UseCaseGetUsersByFilter(IMapper mapper, IUserRepository userRepository, IFriendRepository friendRepository, IFriendRequestRepository friendRequestRepository, IAccountRepository accountRepository)
    {
        _mapper = mapper;
        
        _userRepository = userRepository;
        _friendRepository = friendRepository;
        _friendRequestRepository = friendRequestRepository;
        _accountRepository = accountRepository;
    }

    public IEnumerable<DtoOutputUserDiscover> Execute(string connectedUserId, int userCount, string searchValue)
    {
        var users = _userRepository
            .FetchUsersByFilter(connectedUserId, searchValue, userCount)
            .Select(user => {
                var acc = _accountRepository.FetchById(user.AccountId);
                var dtoUser = _mapper.Map<DtoOutputUserDiscover>(user);
                dtoUser.IdImage = acc.ImageId;
                return dtoUser;
            })
            .ToList();

        foreach (var user in users)
        {
            if (_friendRepository.IsFriend(connectedUserId, user.Id))
            {
                user.IsFriendWithConnected = 2;
            }
            else if (_friendRequestRepository.IsRequestPresent(user.Id, connectedUserId))
            {
                user.IsFriendWithConnected = 1;
            }
            else if (_friendRequestRepository.IsRequestPresent(connectedUserId, user.Id))
            {
                user.IsFriendWithConnected = 0;
            }
            else
            {
                user.IsFriendWithConnected = -1;
            }
            
            
            user.CommonFriendCount = _friendRepository.FetchCommonFriendsCount(connectedUserId, user.Id);
        }
        
        return users;
    }
}