using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Users;

public class UserRepository: IUserRepository
{
    private readonly MoodContext _context;

    public UserRepository(MoodContext context)
    {
        _context = context;
    }

    public List<DbUser> GetAll()
    {
        return _context.Users.ToList();
    }

    public DbUser Create(DbUser user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public bool Update(DbUser user)
    {
        var entity = _context.Users.FirstOrDefault(e => e.Id == user.Id);

        if (entity == null)
            return false;

        entity.Mail = user.Mail;
        entity.Name = user.Name;
        entity.Title = user.Title;
        
        _context.SaveChanges();

        return true;
    }

    public bool Delete(string id)
    {
        throw new NotImplementedException();
    }

    public DbUser FetchById(string id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);

        if (user == null || user.IsDeleted) throw new KeyNotFoundException("userIdNotFound");

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

    public IEnumerable<DbUser> FetchUsersByFilter(string userIdToIgnore, string nameFilter)
    {
        return _context.Users
            .Where(user => user.Id != userIdToIgnore && user.Name.Contains(nameFilter));
    }
}