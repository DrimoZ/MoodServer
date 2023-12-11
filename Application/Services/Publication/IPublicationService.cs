using Application.Services.Publication.Util;
using Application.Services.Users.Util;
using Infrastructure.EntityFramework.DbComplexEntities;

namespace Application.Services.Publication;

public interface IPublicationService
{
    IEnumerable<Domain.Publication> FetchPublicationsByUserId(string userId);
    Domain.Publication FetchPublicationById(int pubId, IEnumerable<EPublicationFetchAttribute> attributesToFetch);
}