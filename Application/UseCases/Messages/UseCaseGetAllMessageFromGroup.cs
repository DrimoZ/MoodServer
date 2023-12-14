using Application.Dtos.Message;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Repositories.Accounts;
using Infrastructure.EntityFramework.Repositories.Communications;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Messages;

public class UseCaseGetAllMessageFromGroup:IUseCaseParameterizedQuery<IEnumerable<DtoOutputMessage>, int>
{
    private readonly IMapper _mapper;
    private readonly IMessageRepository _messageRepository;
    private readonly IUserGroupRepository _userGroupRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;

    public UseCaseGetAllMessageFromGroup(IMapper mapper, IMessageRepository messageRepository, IUserGroupRepository userGroupRepository, IUserRepository userRepository, IAccountRepository accountRepository)
    {
        _mapper = mapper;
        _messageRepository = messageRepository;
        _userGroupRepository = userGroupRepository;
        _userRepository = userRepository;
        _accountRepository = accountRepository;
    }

    public IEnumerable<DtoOutputMessage> Execute(int groupId)
    {
        List<DtoOutputMessage> messages = new List<DtoOutputMessage>();
        var userGroups = _userGroupRepository.FetchAllByGroupId(groupId);
        foreach (var userGroup in userGroups)
        {
            Console.WriteLine("Les données de l'usergroup"+ userGroup.GroupId + userGroup.UserId);
            var entity = _messageRepository.FetchAllMessageFromUserGroup(userGroup.Id);
            foreach (var message in entity)
            {
                var user = _userRepository.FetchById(userGroup.UserId);
                var account = _accountRepository.FetchById(user.AccountId);
                var msgToAdd = new DtoOutputMessage
                {
                    GroupId = userGroup.GroupId,
                    UserLogin = user.Login,
                    UserId = user.Id,
                    UserName = user.Name,
                    Date = message.Date,
                    Id = message.Id,
                    Content = message.Content,
                    ImageId = account.ImageId
                };
                messages.Add(_mapper.Map<DtoOutputMessage>(msgToAdd));
            }
            messages = messages.OrderBy(msg => msg.Date).ToList();
            Console.WriteLine("oui");
        }
        return messages;
    }
}