using Application.Services.Users.Util;
using AutoMapper;
using Domain;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Repositories.Accounts;
using Infrastructure.EntityFramework.Repositories.Publications;
using Infrastructure.EntityFramework.Repositories.Users;
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

    public User FetchById(string id, IEnumerable<EUserFetchAttribute> attributesToFetch)
    {
        var dbUser = _userRepository.FetchById(id);
        var user = _mapper.Map<User>(dbUser);

        //Factory pour chaque attribut
        foreach (var attribute in attributesToFetch)
        {
            switch (attribute)
            {
                case EUserFetchAttribute.Data:
                        user.FriendCount = _friendRepository.FetchFriendCount(user.Id);
                        user.PublicationCount = _publicationRepository.FetchPublicationCount(user.Id);
                    break;
                case EUserFetchAttribute.Account:
                        var dbAccount = _accountRepository.FetchById(dbUser.AccountId);
                        user.Account = _mapper.Map<Account>(dbAccount);
                    break;
                case EUserFetchAttribute.Friends:
                        var dbFriends = _friendRepository.FetchFriends(user.Id);
                        user.AddRange(dbFriends.Select(dbU => _mapper.Map<Domain.User>(dbU)).ToList());
                    break;
                case EUserFetchAttribute.Publications:
                        var dbPublications = _publicationRepository.FetchUserPublications(user.Id);
                        user.AddRange(dbPublications.Select(dbP => _mapper.Map<Domain.Publication>(dbP)).ToList());
                    break;
                case EUserFetchAttribute.Messages:
                    break;
                default:
                    throw new ArgumentException($"Unknown attribute: {attribute}");
            }
        }

        return user;
    }

    public User FetchOtherById(string id, IEnumerable<EUserFetchAttribute> attributesToFetch, bool isFriend)
    {
        var dbUser = _userRepository.FetchById(id);
        var user = _mapper.Map<User>(dbUser);

        //Factory pour chaque attribut
        foreach (var attribute in attributesToFetch)
        {
            switch (attribute)
            {
                case EUserFetchAttribute.Data:
                    if(user.IsPublic || isFriend)
                    {
                        user.FriendCount = _friendRepository.FetchFriendCount(user.Id);
                        user.PublicationCount = _publicationRepository.FetchPublicationCount(user.Id);
                    }
                    user.Id = "";
                    user.Account = null;
                    user.Role = 0;
                    user.Mail = "";
                    break;
                case EUserFetchAttribute.Account:
                    if(isFriend || user.IsPublic)
                    {
                        var dbAccount = _accountRepository.FetchById(dbUser.AccountId);
                        user.Account = _mapper.Map<Account>(dbAccount);
                        user.Account.Id = "";
                        user.Account.PhoneNumber = "";
                    }
                    break;
                case EUserFetchAttribute.Friends:
                    if(isFriend || user.IsFriendPublic)
                    {
                        var dbFriends = _friendRepository.FetchFriends(user.Id);
                        user.AddRange(dbFriends.Select(dbU => _mapper.Map<User>(dbU)).ToList());
                    }
                    break;
                case EUserFetchAttribute.Publications:
                    if(isFriend || user.IsPublicationPublic)
                    {
                        var dbPublications = _publicationRepository.FetchUserPublications(user.Id);
                        user.AddRange(dbPublications.Select(dbP => _mapper.Map<User>(dbP)).ToList());
                    }
                    break;
                default:
                    throw new ArgumentException($"Unknown attribute: {attribute}");
            }
        }

        return user;
    }
}