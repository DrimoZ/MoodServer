using Domain;

namespace Application.Services.Users.Util;

public interface IPublicationService
{
    Publication FetchById(int id, IEnumerable<EPublicationFetchAttribute> attributesToFetch);
}