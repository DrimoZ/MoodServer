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

    public List<DbUser> GetAll()
    {
        throw new NotImplementedException();
    }

    public DbUser Create(DbUser user)
    {
        throw new NotImplementedException();
    }

    public bool Update(DbUser user)
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

        if (user == null) throw new KeyNotFoundException($"userLoginOrMailNotFound");

        return user;
    }
    
    public DbUser FetchByLoginAndMail(string login, string mail)
    {
        var user = _context.Users.FirstOrDefault(u => u.Login == login || u.Mail == mail);

        if (user == null) throw new KeyNotFoundException($"userLoginAndMailNotFound");

        return user;
    }

    public DbUser FetchByName(string name)
    {
        var user = _context.Users.FirstOrDefault(u => u.Name == name);

        if (user == null) throw new KeyNotFoundException($"userNameNotFound");

        return user;
    }
    
    public DbUser FetchByLogin(string login)
    {
        var user = _context.Users.FirstOrDefault(u => u.Login == login);

        if (user == null) throw new KeyNotFoundException($"userLoginNotFound");

        return user;
    }
    
    public DbUser FetchByMail(string mail)
    {
        var user = _context.Users.FirstOrDefault(u => u.Mail == mail);

        if (user == null) throw new KeyNotFoundException($"userMailNotFound");

        return user;
    }
}