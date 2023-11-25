using Domain;

namespace Infrastructure.Repositories;

public interface IUserRepository
{
    User? Get(int id);
    List<User> GetAll();
    User Create(User user);
    bool Update(User user);
    bool Delete(int id);
}