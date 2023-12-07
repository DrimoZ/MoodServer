using Application.Dtos.User.UserAuthentication;
using Application.Dtos.User.UserData;
using Application.UseCases.Utils;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.UnitOfWork;

namespace Application.UseCases.Users.UserData;

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
        _unitOfWork.BeginTransaction();
        var entity = _userRepository.FetchById(input.Id);
        entity.Mail = input.Mail;
        entity.Title = input.Title;
        entity.Name = input.Name;
        Console.WriteLine("oui");
        try
        {
            var account = _accountRepository.FetchById(entity.AccountId);
            account.Description = input.Description;
            account.BirthDate = input.Birthdate;
            _accountRepository.Update(account);
        }
        catch (Exception e)
        {
            _unitOfWork.Rollback();
            return false;
        }

        _userRepository.Update(entity);
        _unitOfWork.Commit();
        return true;
    }
}