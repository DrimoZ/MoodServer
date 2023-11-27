using Domain;
using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public class AccountRepository:IAccountRepository
{
    private readonly MoodContext _context;

    public AccountRepository(MoodContext context)
    {
        _context = context;
    }

    public Account create(Account a)
    {
        return null!;
    }

    public DbAccount FetchById(int id)
    {
        var account = _context.Accounts.FirstOrDefault(a => a.Id == id);
        if (account == null) throw new KeyNotFoundException($"Account Not Found");

        return account;
    }
}