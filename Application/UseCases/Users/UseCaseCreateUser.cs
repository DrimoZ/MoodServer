using Application.Dtos.User;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Users;

public class UseCaseCreateUser: IUseCaseParameterizedQuery<DbUser, DtoInputSignUpUser>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;

    public UseCaseCreateUser(IMapper mapper, IUserRepository userRepository, IAccountRepository accountRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _accountRepository = accountRepository;
    }

    public DbUser Execute(DtoInputSignUpUser input)
    {
        //Unit of work => gestion de transactions sur db
        
        var dbAccount = _accountRepository.Create(_mapper.Map<DbAccount>(input)); //ignorer l'id dans mapper + l'init dans un while
        
        
        var user = _mapper.Map<DtoInputCreateUser>(input);
        user.Account = _mapper.Map<DtoInputCreateUser.DtoAccount>(dbAccount);

        var identifiedUser = _mapper.Map<DbUser>(user);
        return _userRepository.Create(identifiedUser);
    }
}