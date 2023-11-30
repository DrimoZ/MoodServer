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
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IAccountRepository accountRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _accountRepository = accountRepository;
        _mapper = mapper;
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
                    var dbFriends = _userRepository.FetchFriends(id);
                    user.AddRange(dbFriends.Select(dbU => _mapper.Map<User>(dbU)).ToList());
                    break;
                case UserFetchAttribute.Publications:
                    //user.AddRange(_userRepository.FetchPublications(id));
                    break;
                case UserFetchAttribute.Messages:
                    break;
                default:
                    throw new ArgumentException($"Unknown attribute: {attribute}");
            }
        }

        return user;
    }
}