using Application.Dtos.Message;
using Application.UseCases.Utils;
using AutoMapper;
using Domain.Exception;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Repositories.Accounts;
using Infrastructure.EntityFramework.Repositories.Communications;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Messages;

public class UseCaseGetMessageFromGroup:IUseCaseParameterizedQuery<IEnumerable<DtoOutputMessage>, int, int>
{
    private readonly IMapper _mapper;
    private readonly IMessageRepository _messageRepository;
    private readonly IUserGroupRepository _userGroupRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;

    public UseCaseGetMessageFromGroup(IMapper mapper, IMessageRepository messageRepository, IUserGroupRepository userGroupRepository, IUserRepository userRepository, IAccountRepository accountRepository)
    {
        _mapper = mapper;
        _messageRepository = messageRepository;
        _userGroupRepository = userGroupRepository;
        _userRepository = userRepository;
        _accountRepository = accountRepository;
    }

    public IEnumerable<DtoOutputMessage> Execute(int groupId, int showCount)
    {
        List<DtoOutputMessage> messages = new List<DtoOutputMessage>();
        var entities = _messageRepository.FetchMessageGroup(groupId, showCount);
        foreach (var dbMessage in entities)
        {
            var userGroup = _userGroupRepository.FetchById(dbMessage.UserGroupId);
            DtoOutputMessage msg;
            try
            {
                var user = _userRepository.FetchById(userGroup.UserId);
                var account = _accountRepository.FetchById(user.AccountId);
                msg = new DtoOutputMessage
                {
                    UserLogin = user.UserLogin,
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Date = dbMessage.Date,
                    Id = dbMessage.Id,
                    Content = dbMessage.Content,
                    ImageId = account.ImageId,
                    IsDeleted = dbMessage.IsDeleted,
                    HasLeft = userGroup.HasLeft
                };
                messages.Add(msg);
            }
            catch (DeletedUserException e)
            {
                msg = new DtoOutputMessage
                {
                    UserLogin = "User Deleted",
                    UserId = userGroup.UserId,
                    UserName = "User Deleted",
                    Date = dbMessage.Date,
                    Id = dbMessage.Id,
                    Content = dbMessage.Content,
                    IsDeleted = dbMessage.IsDeleted,
                    HasLeft = userGroup.HasLeft
                };
                messages.Add(msg);
            }
            
        }
        return messages;
    }
}