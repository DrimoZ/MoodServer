using Application.UseCases.Utils;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Repositories.Publications;

namespace Application.UseCases.Publications;

public class UseCaseSetPublicationDeleted:IUseCaseWriter<bool, int>
{
    private readonly IPublicationRepository _publicationRepository;

    public UseCaseSetPublicationDeleted(IPublicationRepository publicationRepository)
    {
        _publicationRepository = publicationRepository;
    }

    public bool Execute(int id)
    {
        return _publicationRepository.UpdateDelete(id, true);
    }
}