using Application.Dtos.Images;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Repositories.Accounts;
using Infrastructure.EntityFramework.Repositories.Users;
using Infrastructure.EntityFramework.UnitOfWork;

namespace Application.UseCases.Users.UserData;

public class UseCaseUpdateUserProfilePicture:IUseCaseWriter<bool, string, DtoInputImage>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IImageRepository _imageRepository;
    private readonly IMapper _mapper;

    public UseCaseUpdateUserProfilePicture(IUnitOfWork unitOfWork, IUserRepository userRepository, IAccountRepository accountRepository, IImageRepository imageRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _accountRepository = accountRepository;
        _imageRepository = imageRepository;
        _mapper = mapper;
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
        catch (Exception e)
        {
            _unitOfWork.Rollback();
            return false;
        }

        _unitOfWork.Commit();
        return true;
    }
}