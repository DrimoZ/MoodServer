using Application.Dtos.User.UserAuthentication;
using Application.Services.Utils;
using Application.UseCases.Utils;
using AutoMapper;
using Domain;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories.Accounts;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Users.UserAuthentication;

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
        var id = "";
        
        //Create a dbAccount 
        var dbAccount = _mapper.Map<DbAccount>(input);
        
        try
        {
            //Generate Custom Id
            do { id = IdService.Generate32CharId(EClassType.Account); } 
            while (_accountRepository.FetchById(id) is { } found);
        }
        catch (KeyNotFoundException)
        {
            //Apply Id to dbAccount
            dbAccount.Id = id;
        }
        
        //Create a dto For the user
        var dtoUser = _mapper.Map<DtoInputCreateUser>(input);
        dtoUser.AccountId = dbAccount.Id;
        dtoUser.Password = BCryptService.HashPassword(dtoUser.Password);
        
        //Create a dbUser
        var dbUser = _mapper.Map<DbUser>(dtoUser);
        try
        {
            //Generate Custom Id
            do { id = IdService.Generate32CharId(EClassType.User); } 
            while (_userRepository.FetchById(id) is { } user1);
        }
        catch (KeyNotFoundException)
        {
            //Apply Id to dbUser
            dbUser.Id = id;
        }
        
        //Apply Role to dbUser
        dbUser.Role = (int)EUserRole.User;
        
        //Adding Account and User to DB
        _accountRepository.Create(dbAccount);
        return _userRepository.Create(dbUser);
    }
}