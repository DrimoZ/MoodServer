using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories.Communications;

namespace Application.Dtos.Message;

public class UseCaseDeleteMessageById: IUseCaseWriter<bool, int>
{
    private IMessageRepository _messageRepository;
    private IMapper _mapper;

    public UseCaseDeleteMessageById(IMessageRepository messageRepository, IMapper mapper)
    {
        _messageRepository = messageRepository;
        _mapper = mapper;
    }

    public bool Execute(int id)
    {
        return _messageRepository.Delete(id);
    }
}