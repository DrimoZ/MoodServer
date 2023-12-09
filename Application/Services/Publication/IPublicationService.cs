using Application.Services.Publication.Util;
using Application.Services.Users.Util;

namespace Application.Services.Publication;

public interface IPublicationService
{
    Domain.Publication FetchById(int id, IEnumerable<EPublicationFetchAttribute> attributesToFetch);
}