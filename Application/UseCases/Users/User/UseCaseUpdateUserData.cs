using System.Data;
using Application.Dtos.User.UserData;
using Application.UseCases.Utils;
using Infrastructure.EntityFramework.Repositories.Accounts;
using Infrastructure.EntityFramework.Repositories.Users;
using Infrastructure.EntityFramework.UnitOfWork;

namespace Application.UseCases.Users.User;

public class UseCaseUpdateUserData:IUseCaseWriter<bool, DtoInputUpdateUser>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;

    public UseCaseUpdateUserData(IUnitOfWork unitOfWork, IUserRepository userRepository, IAccountRepository accountRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _accountRepository = accountRepository;
    }

    public bool Execute(DtoInputUpdateUser input)
    {
        try
        {
            _userRepository.FetchByLoginOrMail(input.Mail);
            throw new DuplicateNameException("DuplicateMailInDB");
        }
        catch (Exception e)
        {
            if (e.Message != "userLoginOrMailNotFound") return false;
            
            _unitOfWork.BeginTransaction();
            var entity = _userRepository.FetchById(input.Id);
        
            entity.Mail = input.Mail;
            entity.Title = input.Title;
            entity.Name = input.Name;
        
            try
            {
                var account = _accountRepository.FetchById(entity.AccountId);
                account.Description = input.Description;
                account.BirthDate = input.Birthdate;
                _accountRepository.Update(account);
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
}