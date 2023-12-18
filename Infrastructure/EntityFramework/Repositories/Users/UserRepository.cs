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
        _context.SaveChanges();
        return user;
    }

    public bool Update(DbUser user)
    {
        var entity = _context.Users.Where(u => !u.IsDeleted).FirstOrDefault(e => e.Id == user.Id);

        if (entity == null)
            return false;

        entity.Mail = user.Mail;
        entity.Name = user.Name;
        entity.Title = user.Title;

        entity.IsPublic = user.IsPublic;
        entity.IsFriendPublic = user.IsFriendPublic;
        entity.IsPublicationPublic = user.IsPublicationPublic;

        entity.Password = user.Password;
        
        _context.SaveChanges();

        return true;
    }

    public bool Delete(string id)
    {
        var entity = _context.Users.Where(u => !u.IsDeleted).FirstOrDefault(e => e.Id == id);

        if (entity == null)
            return false;

        entity.IsDeleted = true;

        _context.SaveChanges();

        return true;
    }

    public DbUser FetchById(string id)
    {
        var user = _context.Users.Where(u => !u.IsDeleted).FirstOrDefault(u => u.Id == id);

        if (user == null || user.IsDeleted) throw new KeyNotFoundException("userIdNotFound");

        return user;
    }

    public DbUser FetchByLoginOrMail(string login)
    {
        var user = _context.Users.Where(u => !u.IsDeleted).FirstOrDefault(u => u.Login == login || u.Mail == login);

        if (user == null) throw new KeyNotFoundException($"userLoginOrMailNotFound");

        return user;
    }
    
    public DbUser FetchByLoginAndMail(string login, string mail)
    {
        var user = _context.Users.Where(u => !u.IsDeleted).FirstOrDefault(u => u.Login == login || u.Mail == mail);

        if (user == null) throw new KeyNotFoundException($"userLoginAndMailNotFound");

        return user;
    }
    
    public DbUser FetchByLogin(string login)
    {
        var user = _context.Users.Where(u => !u.IsDeleted).FirstOrDefault(u => u.Login == login);

        if (user == null) throw new KeyNotFoundException($"userLoginNotFound");

        return user;
    }

    public IEnumerable<DbUser> FetchUsersByFilter(string userIdToIgnore, string nameFilter, int userCount)
    {
        return _context.Users
            .Where(user => !user.IsDeleted && user.Id != userIdToIgnore && user.Name.Contains(nameFilter))
            .AsEnumerable()
            .Reverse()
            .Take(userCount);
    }
}