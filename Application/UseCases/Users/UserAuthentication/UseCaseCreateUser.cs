using Application.Dtos.User.UserAuthentication;
using Application.Services.Utils;
using Application.UseCases.Utils;
using AutoMapper;
using Domain;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories.Accounts;
using Infrastructure.EntityFramework.Repositories.Users;
using Infrastructure.EntityFramework.UnitOfWork;

namespace Application.UseCases.Users.UserAuthentication;

public class UseCaseCreateUser: IUseCaseParameterizedQuery<DtoUserAuthenticate?, DtoInputUserSignUp>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;


    public UseCaseCreateUser(IMapper mapper, IUserRepository userRepository, IAccountRepository accountRepository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public DtoUserAuthenticate? Execute(DtoInputUserSignUp inputUser)
    {
        var id = "";
        try
        {
            _userRepository.FetchByLoginAndMail(inputUser.UserLogin, inputUser.UserMail);
            throw new Exception("Mail or Login already used");
        }
        catch (Exception e)
        {
            // ignored
        }

        //Create a dbAccount 
        var dbAccount = _mapper.Map<DbAccount>(inputUser);
        
        try
        {
            //Generate Custom Id
            do { id = IdService.Generate32CharId(EClassType.Account); } 
            while (_accountRepository.FetchById(id) is { } found);
        }
        catch (KeyNotFoundException)
        {
            //Apply Id to dbAccount
            dbAccount.AccountId = id;
        }
        
        //Create a dto For the user
        var dtoUser = _mapper.Map<DtoInputUserCreate>(inputUser);
        dtoUser.AccountId = dbAccount.AccountId;
        dtoUser.UserPassword = BCryptService.HashPassword(dtoUser.UserPassword);
        
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
            dbUser.UserId = id;
        }
        
        //Apply Role & Privacy to dbUser
        dbUser.UserRole = (int)EUserRole.User;
        dbUser.IsPublic = true;
        dbUser.IsFriendPublic = true;
        dbUser.IsPublicationPublic = true;
        
        
        //Adding Account and User to DB
        _unitOfWork.BeginTransaction();
        try
        {
            _accountRepository.Create(dbAccount);
            dbUser = _userRepository.Create(dbUser);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            _unitOfWork.Rollback();
            return null;
        }

        
        _unitOfWork.Commit();
        return _mapper.Map<DtoUserAuthenticate>(dbUser);
    }
}