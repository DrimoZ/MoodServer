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
    private readonly IPublicationRepository _publicationRepository;
    private readonly IMapper _mapper;

    public UserService(IMapper mapper, IUserRepository userRepository, IAccountRepository accountRepository, IFriendRepository friendRepository, IPublicationRepository publicationRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _accountRepository = accountRepository;
       
        _friendRepository = friendRepository;
        _publicationRepository = publicationRepository;
    }

    public User FetchById(string id, IEnumerable<UserFetchAttribute> attributesToFetch)
    {
        var dbUser = _userRepository.FetchById(id);
        var user = _mapper.Map<User>(dbUser);

        foreach (var attribute in attributesToFetch)
        {
            switch (attribute)
            {
                case UserFetchAttribute.Data:
                    user.FriendCount = _friendRepository.FetchFriendCount(user.Id);
                    user.PublicationCount = _publicationRepository.FetchPublicationCount(user.Id);
                    break;
                case UserFetchAttribute.Account:
                    var dbAccount = _accountRepository.FetchById(dbUser.AccountId);
                    user.Account =  _mapper.Map<Account>(dbAccount);
                    break;
                case UserFetchAttribute.Friends:
                    var dbFriends = _friendRepository.FetchFriends(user.Id);
                    user.AddRange(dbFriends.Select(dbU => _mapper.Map<User>(dbU)).ToList());
                    break;
                case UserFetchAttribute.Publications:
                    var dbPublications = _publicationRepository.FetchPublications(user.Id);
                    user.AddRange(dbPublications.Select(dbP => _mapper.Map<User>(dbP)).ToList());
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