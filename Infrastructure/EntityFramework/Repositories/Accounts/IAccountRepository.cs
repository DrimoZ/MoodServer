using Domain;
using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public interface IAccountRepository
{
    bool Update(DbAccount account);
    DbAccount Create(DbAccount account);
    DbAccount FetchById(string  id);
    
}