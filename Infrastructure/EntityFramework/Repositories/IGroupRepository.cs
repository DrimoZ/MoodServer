using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public interface IGroupRepository
{
    DbGroup Create(DbGroup map, IEnumerable<string> userIds);
}