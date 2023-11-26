using Domain;
using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public interface IUserRepository
{
    User? Get(int id);
    List<User> GetAll();
    User Create(User user);
    bool Update(User user);
    bool Delete(int id);

    DbUser FetchById(int id);
    DbUser FetchByLoginOrMail(string login);
}