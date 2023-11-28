using Domain;
using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public interface IAccountRepository
{
    DbAccount Create(DbAccount account);
    DbAccount FetchById(string  id);
    
}