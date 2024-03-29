using Application.Dtos.User.User;
using Application.Services.Utils;
using Application.UseCases.Utils;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Users.User;

public class UseCaseUpdateUserPassword: IUseCaseParameterizedWriter<bool, string, DtoInputUpdateUserPassword>
{
    private readonly IUserRepository _userRepository;

    public UseCaseUpdateUserPassword(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public bool Execute(string connectedUserId, DtoInputUpdateUserPassword dto)
    {
        var user = _userRepository.FetchById(connectedUserId);

        if (dto.NewPassword == dto.OldPassword)
            return false;
        
        if (!BCryptService.VerifyPassword(dto.OldPassword, user.UserPassword))
            return false;

        user.UserPassword = BCryptService.HashPassword(dto.NewPassword);

        return _userRepository.Update(user);
    }
}