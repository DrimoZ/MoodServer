using Application.UseCases.Utils;
using Infrastructure.EntityFramework.Repositories.Publications;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Publications;

public class UseCaseDeleteCommentInPublicationById: IUseCaseParameterizedWriter<bool, string, int>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;

    public UseCaseDeleteCommentInPublicationById(ICommentRepository commentRepository, IUserRepository userRepository)
    {
        _commentRepository = commentRepository;
        _userRepository = userRepository;
    }

    public bool Execute(string connectedUserId, int idComment)
    {
        var dbComment = _commentRepository.FetchById(idComment);
        if (connectedUserId == dbComment.UserId)
            return _commentRepository.Delete(dbComment.CommentId);
        
        var dbConnectedUser = _userRepository.FetchById(connectedUserId);
        var dbCommentAuthor = _userRepository.FetchById( dbComment.UserId);

        return dbConnectedUser.UserRole > dbCommentAuthor.UserRole && _commentRepository.Delete(dbComment.CommentId);
    }
}