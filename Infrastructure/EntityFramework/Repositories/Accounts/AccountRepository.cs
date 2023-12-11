using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Accounts;

public class AccountRepository:IAccountRepository
{
    private readonly MoodContext _context;

    public AccountRepository(MoodContext context)
    {
        _context = context;
    }


    public bool Update(DbAccount account)
    {
        var entity = _context.Accounts.FirstOrDefault(a => a.Id == account.Id);
        if (entity == null)
            return false;
        entity.Description = account.Description;
        entity.BirthDate = account.BirthDate;
        
        _context.SaveChanges();
            
        return true;
    }

    public DbAccount Create(DbAccount account)
    {
        _context.Accounts.Add(account);
        _context.SaveChanges();
        return account;  
    }

    public DbAccount FetchById(string id)
    {
        var account = _context.Accounts.FirstOrDefault(a => a.Id == id);
        if (account == null) throw new KeyNotFoundException($"Account Not Found");
        return account;
    }
}