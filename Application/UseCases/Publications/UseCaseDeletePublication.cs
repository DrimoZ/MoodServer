using Application.UseCases.Utils;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Publications;

public class UseCaseDeletePublication:IUseCaseParameterizedQuery<bool, int>
{
    private readonly IPublicationRepository _publicationRepository;

    public UseCaseDeletePublication(IPublicationRepository publicationRepository)
    {
        _publicationRepository = publicationRepository;
    }

    public bool Execute(int id)
    {
        return _publicationRepository.Delete(id);
    }
}