using Domain.Exception;
using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Users;

public class UserRepository: IUserRepository
{
    private readonly MoodContext _context;

    public UserRepository(MoodContext context)
    {
        _context = context;
    }

    public DbUser Create(DbUser user)
    {
        _context.Users.Add(user);
        return user;
    }

    public bool Update(DbUser user)
    {
        var entity = _context.Users.FirstOrDefault(u => u.UserId == user.UserId && !u.IsDeleted);

        if (entity == null)
            return false;

        entity.UserMail = user.UserMail;
        entity.UserName = user.UserName;
        entity.UserTitle = user.UserTitle;

        entity.IsPublic = user.IsPublic;
        entity.IsFriendPublic = user.IsFriendPublic;
        entity.IsPublicationPublic = user.IsPublicationPublic;

        entity.UserPassword = user.UserPassword;
        
        _context.SaveChanges();

        return true;
    }

    public bool Delete(string id)
    {
        var entity = _context.Users.FirstOrDefault(u => u.UserId == id && !u.IsDeleted);

        if (entity == null)
            return false;

        entity.IsDeleted = true;

        _context.SaveChanges();

        return true;
    }

    public DbUser FetchById(string id)
    {
        var user = _context.Users
            .FirstOrDefault(u => u.UserId == id);

        if (user == null) throw new KeyNotFoundException("userNotFound");
        if (user.IsDeleted) throw new DeletedUserException(user.UserId + " is marked as deleted");
        
        return user;
    }

    public DbUser FetchByLoginOrMail(string login)
    {
        var user = _context.Users.FirstOrDefault(u => (u.UserLogin == login || u.UserMail == login) && !u.IsDeleted);

        if (user == null) throw new KeyNotFoundException($"userNotFound");

        return user;
    }
    
    public DbUser FetchByLoginAndMail(string login, string mail)
    {
        var user = _context.Users.FirstOrDefault(u => (u.UserLogin == login || u.UserMail == mail) && !u.IsDeleted);

        if (user == null) throw new KeyNotFoundException($"userNotFound");

        return user;
    }
    
    public DbUser FetchByLogin(string login)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserLogin == login && !u.IsDeleted);

        if (user == null) throw new KeyNotFoundException($"userNotFound");

        return user;
    }

    public IEnumerable<DbUser> FetchUsersByFilter(string userIdToIgnore, string nameFilter, int userCount)
    {
        return _context.Users
            .Where(user => !user.IsDeleted && user.UserId != userIdToIgnore && user.UserName.Contains(nameFilter.ToLower()))
            .AsEnumerable()
            .Reverse()
            .Take(userCount);
    }

    public bool CheckDuplicatedMail(string userId, string mail)
    {
        var user = _context.Users.FirstOrDefault(u => !u.IsDeleted && u.UserMail == mail);
        return user != null && userId != user.UserId;
    }
}