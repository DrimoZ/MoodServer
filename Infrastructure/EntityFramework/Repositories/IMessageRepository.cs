using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public interface IMessageRepository
{
    public DbMessage Create(DbMessage message);
}