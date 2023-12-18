using Application.Dtos.User.UserData;
using Application.UseCases.Utils;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Users.User;

public class UseCasePatchUser: IUseCaseParameterizedWriter<DbUser, string, DtoInputPatchUserPrivacy>
{
    private readonly IUserRepository _userRepository;

    public UseCasePatchUser(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public DbUser Execute(string connectedUserId, DtoInputPatchUserPrivacy patch)
    {
        var user = _userRepository.FetchById(connectedUserId);

        switch (patch.Path)
        {
            case "isPublic":
                user.IsPublic = patch.Value;
                break;
            case "isFriendPublic":
                user.IsFriendPublic = patch.Value;
                break;
            case "isPublicationPublic":
                user.IsPublicationPublic = patch.Value;
                break;
        }

        _userRepository.Update(user);

        return user;
    }
}