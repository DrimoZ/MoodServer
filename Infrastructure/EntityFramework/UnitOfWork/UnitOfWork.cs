using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.EntityFramework.UnitOfWork;

public class UnitOfWork:IUnitOfWork
{
    private readonly MoodContext _context;
    private IDbContextTransaction _transaction; 

    public UnitOfWork(MoodContext context)
    {
        _context = context;
    }

    public void BeginTransaction()
    {
        _transaction = _context.Database.BeginTransaction();
    }

    public void Commit()
    {
        _transaction.Commit();
    }

    public void Rollback()
    {
        _transaction.Rollback();
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}