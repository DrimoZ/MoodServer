using Application.UseCases.Utils;
using Infrastructure.EntityFramework.Repositories.Publications;

namespace Application.UseCases.Publications;

public class UseCaseDeleteCommentInPublicationById: IUseCaseParameterizedWriter<bool, string, int>
{
    private readonly ICommentRepository _commentRepository;

    public UseCaseDeleteCommentInPublicationById(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public bool Execute(string connectedUserId, int idComment)
    {
        var dbComment = _commentRepository.FetchById(idComment);

        return dbComment.IdUser == connectedUserId && _commentRepository.Delete(dbComment.Id);
    }
}