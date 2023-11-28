using Domain;
using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public interface IUserRepository
{
    List<DbUser> GetAll();
    DbUser Create(DbUser user);
    bool Update(DbUser user);
    bool Delete(int id);

    DbUser FetchById(int id);
    DbUser FetchByLoginOrMail(string login);
    DbUser FetchByName(string name);
    
    
}