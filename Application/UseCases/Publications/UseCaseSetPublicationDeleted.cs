using Application.UseCases.Utils;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Repositories.Publications;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Publications;

public class UseCaseSetPublicationDeleted:IUseCaseParameterizedWriter<bool, string, int>
{
    private readonly IPublicationRepository _publicationRepository;
    private readonly IUserRepository _userRepository;

    public UseCaseSetPublicationDeleted(IPublicationRepository publicationRepository, IUserRepository userRepository)
    {
        _publicationRepository = publicationRepository;
        _userRepository = userRepository;
    }

    public bool Execute(string connectedUserId, int id)
    {
        var dbPub = _publicationRepository.FetchById(id);
        
        if (connectedUserId == dbPub.UserId)
            return _publicationRepository.UpdateDelete(id, true);

        var dbConnectedUser = _userRepository.FetchById(connectedUserId);
        var dbPubAuthor = _userRepository.FetchById( dbPub.UserId);
        return dbConnectedUser.UserRole > dbPubAuthor.UserRole && _publicationRepository.UpdateDelete(id, true);
    }
}