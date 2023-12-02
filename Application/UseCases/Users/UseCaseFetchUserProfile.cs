using Application.Services.Users;
using Application.Services.Users.Util;
using Application.UseCases.Utils;
using Domain;

namespace Application.UseCases.Users;

public class UseCaseFetchUserProfile: IUseCaseParameterizedQuery<User, string>
{
    private readonly IUserService _userService;

    public UseCaseFetchUserProfile(IUserService userService)
    {
        _userService = userService;
    }

    public User Execute(string userId)
    {
        return _userService.FetchById(userId, new[] { EUserFetchAttribute.Data, EUserFetchAttribute.Account});
    }
}