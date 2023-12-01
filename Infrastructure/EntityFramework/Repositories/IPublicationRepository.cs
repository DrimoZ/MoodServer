using Domain;
using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public interface IPublicationRepository
{
    DbPublication? Get(int id);
    DbPublication Create(DbPublication publication);
    bool Update(DbPublication publication);
    bool Delete(int id);
    public bool UpdateDelete(int id, bool isDeleted);

    IEnumerable<DbPublication> FetchPublications(string userId);
    DbPublication FetchById(int id);
    int FetchPublicationCount(string userId);
}