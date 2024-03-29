using Application.Dtos.Group;
using Application.UseCases.Utils;
using AutoMapper;
using Domain.Exception;
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
        var entities = _userGroupRepository.FetchAllByGroupId(groupId);
        var users = new List<DtoOutputUserFromGroup>();
        foreach (var dbUserGroup in entities)
        {
            if (dbUserGroup.HasLeft == true) continue;
            try
            {
                var user = _userRepository.FetchById(dbUserGroup.UserId);
                var account = _accountRepository.FetchById(user.AccountId);
                var dtoOutputUserFromGroup = new DtoOutputUserFromGroup
                {
                    id = user.UserId,
                    Login = user.UserLogin,
                    Name = user.UserName,
                    ImageId = account.ImageId
                };
                users.Add(dtoOutputUserFromGroup);
            }
            catch (DeletedUserException e) {}
            catch (KeyNotFoundException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        return users;
    }
}