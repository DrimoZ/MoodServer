using Application.UseCases.Utils;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Users.User;

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