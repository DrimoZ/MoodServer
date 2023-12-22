using Application.UseCases.Utils;
using Infrastructure.EntityFramework.Repositories.Communications;

namespace Application.UseCases.Messages;

public class UseCaseSetMessageIsDeleted:IUseCaseParameterizedQuery<bool, int>
{
    private readonly IMessageRepository _messageRepository;

    public UseCaseSetMessageIsDeleted(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public bool Execute(int param)
    {
        return _messageRepository.SetMessageIsDeleted(param);
    }
}