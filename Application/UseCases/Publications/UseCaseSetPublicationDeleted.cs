using Application.UseCases.Utils;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Publications;

public class UseCaseSetPublicationDeleted:IUseCaseParameterizedWriter<bool, int, bool>
{
    private readonly PublicationRepository _publicationRepository;

    public UseCaseSetPublicationDeleted(PublicationRepository publicationRepository)
    {
        _publicationRepository = publicationRepository;
    }

    public bool Execute(int id, bool isDeleted)
    {
        return _publicationRepository.UpdateDelete(id, isDeleted);
    }
}