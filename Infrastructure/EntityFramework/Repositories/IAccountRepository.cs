using Domain;
using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public interface IAccountRepository
{
    DbAccount create(DbAccount account);
    DbAccount FetchById(int  Id);
    
}