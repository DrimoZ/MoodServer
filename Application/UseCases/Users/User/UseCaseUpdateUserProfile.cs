using System.Data;
using Application.Dtos.User.UserProfile;
using Application.UseCases.Utils;
using Infrastructure.EntityFramework.Repositories.Accounts;
using Infrastructure.EntityFramework.Repositories.Users;
using Infrastructure.EntityFramework.UnitOfWork;

namespace Application.UseCases.Users.User;

public class UseCaseUpdateUserProfile:IUseCaseWriter<bool, DtoInputUserUpdateProfile>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;

    public UseCaseUpdateUserProfile(IUnitOfWork unitOfWork, IUserRepository userRepository, IAccountRepository accountRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _accountRepository = accountRepository;
    }

    public bool Execute(DtoInputUserUpdateProfile inputUser)
    {
        _unitOfWork.BeginTransaction();
        var entity = _userRepository.FetchById(inputUser.UserId);
        
        if (_userRepository.CheckDuplicatedMail(inputUser.UserId, inputUser.UserMail))
            throw new DuplicateNameException("DuplicateMailInDB");
        
        entity.UserMail = inputUser.UserMail;
        entity.UserTitle = inputUser.UserTitle;
        entity.UserName = inputUser.UserName;
        
        try
        {
            var accountEntity = _accountRepository.FetchById(entity.AccountId);
            accountEntity.AccountDescription = inputUser.AccountDescription;
            accountEntity.AccountBirthDate = inputUser.AccountBirthdate;
            _accountRepository.Update(accountEntity);
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();
            return false;
        }

        _userRepository.Update(entity);
        _unitOfWork.Commit();
        return true;
    }
}