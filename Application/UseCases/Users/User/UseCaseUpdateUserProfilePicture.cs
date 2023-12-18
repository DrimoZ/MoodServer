using Application.Dtos.Images;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Repositories.Accounts;
using Infrastructure.EntityFramework.Repositories.Users;
using Infrastructure.EntityFramework.UnitOfWork;

namespace Application.UseCases.Users.User;

public class UseCaseUpdateUserProfilePicture:IUseCaseWriter<bool, string, DtoInputImage>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IImageRepository _imageRepository;

    public UseCaseUpdateUserProfilePicture(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository, IAccountRepository accountRepository, IImageRepository imageRepository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        
        _userRepository = userRepository;
        _accountRepository = accountRepository;
        _imageRepository = imageRepository;
    }

    public bool Execute(string connectedUserId, DtoInputImage input)
    {
        _unitOfWork.BeginTransaction();
        var entity = _userRepository.FetchById(connectedUserId);
        
        try
        {
            var image = _imageRepository.Create(_mapper.Map<DbImage>(input));
            
            var account = _accountRepository.FetchById(entity.AccountId);
            account.ImageId = image.Id;
            _accountRepository.Update(account);
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();
            return false;
        }

        _unitOfWork.Commit();
        return true;
    }
}