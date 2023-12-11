using System.Diagnostics;
using Application.Dtos.User.UserData;
using Application.Dtos.User.UserProfile;
using Application.Services.Users;
using Application.Services.Users.Util;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories.Accounts;
using Infrastructure.EntityFramework.Repositories.Publications;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Users.UserData;

public class UseCaseFetchUserProfileByUserId: IUseCaseParameterizedQuery<DtoOutputUserProfile, string, string>
{
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly IFriendRepository _friendRepository;
    private readonly IPublicationRepository _publicationRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public UseCaseFetchUserProfileByUserId(IUserService userService, IMapper mapper, IUserRepository userRepository, IFriendRepository friendRepository, IPublicationRepository publicationRepository, IAccountRepository accountRepository)
    {
        _userService = userService;
        _mapper = mapper;
        _userRepository = userRepository;
        _friendRepository = friendRepository;
        _publicationRepository = publicationRepository;
        _accountRepository = accountRepository;
    }

    public DtoOutputUserProfile Execute(string connectedUser, string profileRequestUser)
    {
        var dbUser = _userRepository.FetchById(profileRequestUser);
        
        var user = _mapper.Map<DtoOutputUserProfile>(dbUser);

        user.IsConnectedUser = connectedUser == profileRequestUser;
        user.FriendCount = _friendRepository.FetchFriendCount(profileRequestUser);
        user.PublicationCount = _publicationRepository.FetchPublicationCount(profileRequestUser);
        user.Description = _accountRepository.FetchById(dbUser.AccountId).Description!;

        return user;
    }
}