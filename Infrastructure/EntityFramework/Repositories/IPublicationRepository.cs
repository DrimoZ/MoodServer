using Domain;
using Infrastructure.EntityFramework.DbComplexEntities;
using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public interface IPublicationRepository
{
    DbComplexPublication Create(DbComplexPublication publication);
    bool Update(DbComplexPublication publication);
    bool Delete(int id);
    public bool UpdateDelete(int id, bool isDeleted);

    IEnumerable<DbComplexPublication> FetchPublications(string userId);
    IEnumerable<DbComplexPublication> FetchFriendPublications(string userId);
    DbComplexPublication FetchById(int id);
    int FetchPublicationCount(string userId);
}