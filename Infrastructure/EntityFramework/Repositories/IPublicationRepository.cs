using Domain;
using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public interface IPublicationRepository
{
    Publication? Get(int id);
    Publication Create(DbPublication publication);
    bool Update(DbPublication publication);
    bool Delete(string id);

    List<DbPublication> FetchByIdUser(string userId);
}