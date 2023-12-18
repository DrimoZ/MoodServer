using Application.Dtos.User.UserProfile;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories.Accounts;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Users.User;

public class UseCaseFetchUserAccountByUserId: IUseCaseParameterizedQuery<DtoOutputUserAccount, string, string>
{
    private readonly IMapper _mapper;
    
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;

    public UseCaseFetchUserAccountByUserId(IMapper mapper, IUserRepository userRepository, IAccountRepository accountRepository)
    {
        _mapper = mapper;
        
        _userRepository = userRepository;
        _accountRepository = accountRepository;
    }

    public DtoOutputUserAccount Execute(string connectedUserId, string profileRequestUserId)
    {
        var dbUser = _userRepository.FetchById(profileRequestUserId);
        var user = _mapper.Map<DtoOutputUserAccount>(dbUser);
        
        var dbAccount = _accountRepository.FetchById(dbUser.AccountId);
        user.Description = dbAccount.Description!;
        user.BirthDate = dbAccount.BirthDate!;
        user.PhoneNumber = dbAccount.PhoneNumber!;

        return user;
    }
}