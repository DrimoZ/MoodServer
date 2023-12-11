using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Communications;

public interface IGroupRepository
{
    DbGroup Create(DbGroup map);
}