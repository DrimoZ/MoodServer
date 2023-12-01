using Application.Services.Users.Util;
using AutoMapper;
using Domain;
using Infrastructure.EntityFramework.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Services.Users;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IFriendRepository _friendRepository;
    private readonly IMapper _mapper;

    public UserService(IMapper mapper, IUserRepository userRepository, IAccountRepository accountRepository, IFriendRepository friendRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _accountRepository = accountRepository;
       
        _friendRepository = friendRepository;
    }

    public User FetchById(string id, IEnumerable<UserFetchAttribute> attributesToFetch)
    {
        var dbUser = _userRepository.FetchById(id);
        var user = _mapper.Map<User>(dbUser);

        foreach (var attribute in attributesToFetch)
        {
            switch (attribute)
            {
                case UserFetchAttribute.Account:
                    var dbAccount = _accountRepository.FetchById(dbUser.AccountId);
                    user.Account =  _mapper.Map<Account>(dbAccount);
                    break;
                case UserFetchAttribute.Friends:
                    var dbFriends = _friendRepository.FetchFriends(user.Id);
                    user.AddRange(dbFriends.Select(dbU => _mapper.Map<User>(dbU)).ToList());
                    break;
                case UserFetchAttribute.Publications:
                    //user.AddRange(_userRepository.FetchPublications(user.Id));
                    break;
                case UserFetchAttribute.Messages:
                    break;
                case UserFetchAttribute.Data:
                    user.FriendCount = _friendRepository.FetchFriendCount(user.Id);
                    break;
                default:
                    throw new ArgumentException($"Unknown attribute: {attribute}");
            }
        }

        return user;
    }
}