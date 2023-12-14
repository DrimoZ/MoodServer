using Application.Dtos.Message;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Repositories.Communications;
using Infrastructure.EntityFramework.Repositories.Users;
using Infrastructure.EntityFramework.UnitOfWork;

namespace Application.UseCases.Messages;

public class UseCaseCreateMessage:IUseCaseParameterizedWriter<DbMessage, DtoInputMessage, int>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserGroupRepository _userGroupRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UseCaseCreateMessage(IMessageRepository messageRepository, IMapper mapper, IUnitOfWork unitOfWork, IUserGroupRepository userGroupRepository)
    {
        _messageRepository = messageRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userGroupRepository = userGroupRepository;
    }

    public DbMessage Execute(DtoInputMessage input, int idUserGroup)
    {
        _unitOfWork.BeginTransaction();
        try
        {
            _userGroupRepository.FetchById(idUserGroup);
        }
        catch (Exception e)
        {
            _unitOfWork.Rollback();
            Console.WriteLine(e);
            throw;
        }
        
        var message = _messageRepository.Create(_mapper.Map<DbMessage>(input), idUserGroup);
        _unitOfWork.Save();
        _unitOfWork.Commit();
        return message;
    }
}