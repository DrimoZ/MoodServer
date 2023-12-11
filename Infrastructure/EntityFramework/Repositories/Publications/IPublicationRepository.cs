using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Publications;

public interface IPublicationRepository
{
    DbPublication Create(DbPublication publication);
    bool Update(DbPublication publication);
    bool Delete(int id);
    public bool UpdateDelete(int id, bool isDeleted);

    IEnumerable<DbPublication> FetchUserPublications(string userId);
    DbPublication FetchById(int id);
    int FetchPublicationCount(string userId);
}