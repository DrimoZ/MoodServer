using Domain;
using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public interface IAccountRepository
{
    Account create(Account a);
    DbAccount FetchById(int  Id);
    
}