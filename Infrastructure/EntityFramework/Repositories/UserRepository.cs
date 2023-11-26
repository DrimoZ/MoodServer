using Domain;
using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public class UserRepository: IUserRepository
{
    private readonly MoodContext _context;

    public UserRepository(MoodContext context)
    {
        _context = context;
    }

    public User? Get(int id)
    {
        throw new NotImplementedException();
    }

    public List<User> GetAll()
    {
        throw new NotImplementedException();
    }

    public User Create(User user)
    {
        throw new NotImplementedException();
    }

    public bool Update(User user)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public DbUser FetchById(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);

        if (user == null) throw new KeyNotFoundException("userIdNotFound");

        return user;
    }

    public DbUser FetchByLoginOrMail(string login)
    {
        var user = _context.Users.FirstOrDefault(u => u.Login == login || u.Mail == login);

        if (user == null) throw new KeyNotFoundException($"userLoginNotFound");

        return user;
    }
}