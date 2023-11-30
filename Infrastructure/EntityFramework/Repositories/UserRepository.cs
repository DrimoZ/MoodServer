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
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public bool Update(DbUser user)
    {
        throw new NotImplementedException();
    }

    public bool Delete(string id)
    {
        throw new NotImplementedException();
    }

    public DbUser FetchById(string id)
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
    
    public List<DbUser> FetchFriends(string userId)
    {
        var friends = _context.Friends
            .Where(f => f.UserId == userId)
            .Join(_context.Users,
                friend => friend.FriendId,
                user => user.Id,
                (friend, user) => user)
            .ToList();

        return friends;
    }
}