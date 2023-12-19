using Application.Dtos.Message;
using Application.UseCases.Utils;
using AutoMapper;
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
            var user = _userRepository.FetchById(userGroup.UserId);
            var account = _accountRepository.FetchById(user.AccountId);
            var msgToAdd = new DtoOutputMessage
            {
                UserLogin = user.Login,
                UserId = user.Id,
                UserName = user.Name,
                Date = dbMessage.Date,
                Id = dbMessage.Id,
                Content = dbMessage.Content,
                ImageId = account.ImageId
            };
            messages.Add(msgToAdd);
        }
        Console.WriteLine("wtf" + messages.Count);
        return messages;
    }
}