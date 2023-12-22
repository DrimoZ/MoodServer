using Application.Dtos.User.User;
using Application.Services.Users;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories.Accounts;
using Infrastructure.EntityFramework.Repositories.Communications;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Users.User;

public class UseCaseFetchUserNotifications: IUseCaseParameterizedQuery<IEnumerable<DtoOutputNotification>, string>
{
    private readonly IMapper _mapper;
    
    private readonly IFriendRequestRepository _friendRequestRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IFriendService _friendService;

    public UseCaseFetchUserNotifications(IFriendRequestRepository friendRequestRepository, IFriendService friendService, IMapper mapper, IAccountRepository accountRepository, IUserRepository userRepository)
    {
        _friendRequestRepository = friendRequestRepository;
        _friendService = friendService;
        _mapper = mapper;
        _accountRepository = accountRepository;
        _userRepository = userRepository;
    }

    public IEnumerable<DtoOutputNotification> Execute(string connectedUserId)
    {
        var dbRequests = _friendRequestRepository.FetchAllRequestByUserId(connectedUserId);

        var notifications = new List<DtoOutputNotification>();

        foreach (var dbFriendRequest in dbRequests)
        {
            try
            {
                var notification = _mapper.Map<DtoOutputNotification>(dbFriendRequest);
                notification.UserId = dbFriendRequest.UserId == connectedUserId
                    ? dbFriendRequest.FriendId
                    : dbFriendRequest.UserId;

                var dbUser = _userRepository.FetchById(notification.UserId);
                notification.UserName = dbUser.UserName;

                var dbAccount = _accountRepository.FetchById(dbUser.AccountId);
                notification.ImageId = dbAccount.ImageId;

                notification.IsConnectedEmitter = connectedUserId == dbFriendRequest.UserId;
                notification.IsFriendWithConnected =
                    _friendService.GetFriendStatus(connectedUserId, notification.UserId);

                notifications.Add(notification);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        return notifications.OrderByDescending(n => n.FriendRequestDate);
    }
}