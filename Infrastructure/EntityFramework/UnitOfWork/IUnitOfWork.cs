using System.Xml.Linq;

namespace Infrastructure.EntityFramework.UnitOfWork;

public interface IUnitOfWork
{
    void BeginTransaction();
    void Commit();
    void Rollback();

    void Save();
}