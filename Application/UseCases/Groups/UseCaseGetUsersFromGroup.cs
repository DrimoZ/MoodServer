using Application.Dtos.Group;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories.Accounts;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Groups;

public class UseCaseGetUsersFromGroup:IUseCaseParameterizedQuery<IEnumerable<DtoOutputUserFromGroup>, int>
{
    private readonly IUserGroupRepository _userGroupRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IUserRepository _userRepository;

    public UseCaseGetUsersFromGroup(IUserGroupRepository userGroupRepository, IUserRepository userRepository, IAccountRepository accountRepository)
    {
        _userGroupRepository = userGroupRepository;
        _userRepository = userRepository;
        _accountRepository = accountRepository;
    }

    public IEnumerable<DtoOutputUserFromGroup> Execute(int groupId)
    {
        Console.WriteLine("ICI");
        var entities = _userGroupRepository.FetchAllByGroupId(groupId);
        Console.WriteLine("ICI");
        var users = new List<DtoOutputUserFromGroup>();
        Console.WriteLine("ICI");
        foreach (var dbUserGroup in entities)
        {
            var user = _userRepository.FetchById(dbUserGroup.UserId);
            var account = _accountRepository.FetchById(user.AccountId);
            var dtoOutputUserFromGroup = new DtoOutputUserFromGroup
            {
                id = user.Id,
                Login = user.Login,
                Name = user.Name,
                ImageId = account.ImageId
            };
            users.Add(dtoOutputUserFromGroup);
        }

        return users;
    }
}