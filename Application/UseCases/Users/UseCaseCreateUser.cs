using Application.Dtos.Account;
using Application.Dtos.User;
using Application.UseCases.Accounts;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Users;

public class UseCaseCreateUser: IUseCaseParameterizedQuery<DbUser, DtoInputSignUpUser>
{
    private readonly ILogger<UseCaseCreateUser> _logger;
    
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly UseCaseCreateAnAccount _useCaseCreateAnAccount;

    public UseCaseCreateUser(IMapper mapper, UseCaseCreateAnAccount useCaseCreateAnAccount, IUserRepository userRepository, ILogger<UseCaseCreateUser> logger)
    {
        _mapper = mapper;
        _useCaseCreateAnAccount = useCaseCreateAnAccount;
        _userRepository = userRepository;
        _logger = logger;
    }

    public DbUser Execute(DtoInputSignUpUser input)
    {
        var accountDto = _useCaseCreateAnAccount.Execute(_mapper.Map<DtoInputCreateAccount>(input));
        var user = _mapper.Map<DtoInputCreateUser>(input);
        user.Account = _mapper.Map<DtoInputCreateUser.DtoAccount>(accountDto);

        var identifiedUser = _mapper.Map<DbUser>(user);
        return _userRepository.Create(identifiedUser);
    }
}