using Domain;

namespace Application.Services.Users.Util;

public interface IPublicationService
{
    Publication FetchById(string id, IEnumerable<EPublicationFetchAttribute> attributesToFetch);
}