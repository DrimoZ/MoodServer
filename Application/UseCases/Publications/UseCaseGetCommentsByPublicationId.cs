using Application.Dtos.Publication;
using Application.UseCases.Utils;
using AutoMapper;
using Domain;
using Infrastructure.EntityFramework.Repositories.Accounts;
using Infrastructure.EntityFramework.Repositories.Publications;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Publications;

public class UseCaseGetCommentsByPublicationId: IUseCaseParameterizedQuery<IEnumerable<DtoOutputPublicationComment>, int>
{
    private readonly IMapper _mapper;
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;

    public UseCaseGetCommentsByPublicationId(IMapper mapper, ICommentRepository commentRepository, IUserRepository userRepository, IAccountRepository accountRepository)
    {
        _mapper = mapper;
        _commentRepository = commentRepository;
        _userRepository = userRepository;
        _accountRepository = accountRepository;
    }

    public IEnumerable<DtoOutputPublicationComment> Execute(int publicationId)
    {
        var dbComments = _commentRepository.FetchCommentsByPublicationId(publicationId);
        var comments = dbComments.Select(dbComment => _mapper.Map<DtoOutputPublicationComment>(dbComment)).ToList();

        foreach (var comment in comments)
        {
            try
            {
                var dbUser = _userRepository.FetchById(comment.AuthorId);
                var dbAccount = _accountRepository.FetchById(dbUser.AccountId);

                comment.AuthorName = dbUser.UserName;
                comment.AuthorImageId = dbAccount.ImageId;
            }
            catch (Exception)
            {
                comments.Remove(comment);
            }
        }

        return comments;
    }
}