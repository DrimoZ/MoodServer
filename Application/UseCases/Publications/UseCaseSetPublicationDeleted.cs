using Application.UseCases.Utils;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Publications;

public class UseCaseSetPublicationDeleted:IUseCaseParameterizedWriter<bool, string, bool>
{
    private readonly PublicationRepository _publicationRepository;

    public UseCaseSetPublicationDeleted(PublicationRepository publicationRepository)
    {
        _publicationRepository = publicationRepository;
    }

    public bool Execute(string id, bool isDeleted)
    {
        return _publicationRepository.UpdateDelete(id, isDeleted);
    }
}