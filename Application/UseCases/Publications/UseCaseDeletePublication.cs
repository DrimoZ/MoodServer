using Application.UseCases.Utils;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Publications;

public class UseCaseDeletePublication:IUseCaseParameterizedQuery<bool, string>
{
    private readonly IPublicationRepository _publicationRepository;

    public UseCaseDeletePublication(IPublicationRepository publicationRepository)
    {
        _publicationRepository = publicationRepository;
    }

    public bool Execute(string id)
    {
        return _publicationRepository.Delete(id);
    }
}