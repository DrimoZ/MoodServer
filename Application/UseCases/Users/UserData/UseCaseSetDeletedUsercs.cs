using Application.Dtos.User.UserData;
using Application.UseCases.Utils;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Users.UserData;

public class UseCaseSetDeletedUser: IUseCaseParameterizedQuery<bool, string>
{
    private readonly IUserRepository _userRepository;

    public UseCaseSetDeletedUser(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public bool Execute(string connectedUserId)
    {
        var user = _userRepository.FetchById(connectedUserId);
        return _userRepository.Delete(user.Id);
    }
}