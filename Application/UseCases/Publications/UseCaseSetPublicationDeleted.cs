using Application.UseCases.Utils;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Publications;

public class UseCaseSetPublicationDeleted:IUseCaseParameterizedWriter<bool, int, bool>
{
    private readonly IPublicationRepository _publicationRepository;

    public UseCaseSetPublicationDeleted(IPublicationRepository publicationRepository)
    {
        _publicationRepository = publicationRepository;
    }

    public bool Execute(int id, bool isDeleted)
    {
        return _publicationRepository.UpdateDelete(id, isDeleted);
    }
}