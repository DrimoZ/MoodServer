using Application.UseCases.Utils;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Users.User;

public class UseCaseDeleteUser: IUseCaseParameterizedQuery<bool, string, string>
{
    private readonly IUserRepository _userRepository;

    public UseCaseDeleteUser(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public bool Execute(string connectedUserId, string userToDelete)
    {
        var dbToDelete = _userRepository.FetchById(userToDelete);
        
        if (connectedUserId == userToDelete)
        {
            return _userRepository.Delete(dbToDelete.UserId);
        }
        
        var dbConnected = _userRepository.FetchById(connectedUserId);
        
        return dbConnected.UserRole > dbToDelete.UserRole && _userRepository.Delete(dbToDelete.UserId);
    }
}