using Domain;
using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public interface IPublicationRepository
{
    DbPublication? Get(int id);
    DbPublication Create(DbPublication publication);
    bool Update(DbPublication publication);
    bool Delete(string id);

    IEnumerable<DbPublication> FetchPublications(string userId);
    int FetchPublicationCount(string userId);
}