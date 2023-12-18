using Application.Dtos.Publication;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories.Publications;

namespace Application.UseCases.Publications;

public class UseCaseCommentPublication: IUseCaseParameterizedWriter<DbComment, string, DtoInputCommentPublication>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public UseCaseCommentPublication(ICommentRepository commentRepository, IMapper mapper)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
    }

    public DbComment Execute(string connectedUserId, DtoInputCommentPublication dto)
    {
        var dbComm = _mapper.Map<DbComment>(dto);
        dbComm.IdUser = connectedUserId;
        
        return _commentRepository.Create(dbComm);
    }
}