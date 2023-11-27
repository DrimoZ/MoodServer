using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public interface IAccountRepository
{
    DbAccount FetchById(int  Id);
}