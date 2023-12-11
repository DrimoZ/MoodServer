using Application.Dtos.User.UserData;
using Application.Dtos.User.UserProfile;
using Application.Services.Users;
using Application.Services.Users.Util;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories.Accounts;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Users.UserData;

public class UseCaseFetchUserAccountByUserId: IUseCaseParameterizedQuery<DtoOutputUserAccount, string, string>
{
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public UseCaseFetchUserAccountByUserId(IUserService userService, IMapper mapper, IUserRepository userRepository, IAccountRepository accountRepository)
    {
        _userService = userService;
        _mapper = mapper;
        _userRepository = userRepository;
        _accountRepository = accountRepository;
    }

    public DtoOutputUserAccount Execute(string connectedUser, string profileRequestUser)
    {
        var dbUser = _userRepository.FetchById(profileRequestUser);
        var user = _mapper.Map<DtoOutputUserAccount>(dbUser);
        
        var dbAccount = _accountRepository.FetchById(dbUser.AccountId);
        user.Description = dbAccount.Description!;
        user.BirthDate = dbAccount.BirthDate!;
        user.PhoneNumber = dbAccount.PhoneNumber!;

        return user;
    }
}