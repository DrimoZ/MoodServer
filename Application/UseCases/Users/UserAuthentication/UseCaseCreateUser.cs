using Application.Dtos.User.UserAuthentication;
using Application.Services.Users;
using Application.Services.Users.Util;
using Application.Services.Utils;
using Application.UseCases.Utils;
using AutoMapper;
using Domain;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Repositories.Accounts;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Users.UserAuthentication;

public class UseCaseCreateUser: IUseCaseParameterizedQuery<DbUser, DtoInputSignUpUser>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;

    private readonly IUserService _userService;

    public UseCaseCreateUser(IMapper mapper, IUserRepository userRepository, IAccountRepository accountRepository, IUserService userService)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _accountRepository = accountRepository;
        _userService = userService;
    }

    public DbUser Execute(DtoInputSignUpUser input)
    {
        var id = "";
        
                            //Unit of work => gestion de transactions sur db
        
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
        dtoUser.Account = _mapper.Map<DtoInputCreateUser.DtoAccount>(dbAccount);
        dtoUser.Password = BCryptService.HashPassword(dtoUser.Password);
        
        //Create a dbUser
        var dbUser = _mapper.Map<DbUser>(dtoUser);
        
        try
        {
            //Generate Custom Id
            do { id = IdService.Generate32CharId(EClassType.User); } 
            while (_userService.FetchById(id, new List<EUserFetchAttribute>()) is { } user1);
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