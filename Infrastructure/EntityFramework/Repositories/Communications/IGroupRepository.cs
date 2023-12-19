using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Communications;

public interface IGroupRepository
{
    DbGroup Create(DbGroup map);
    bool Delete(DbGroup map);
    DbGroup FetchById(int id);

    bool UpdateGroup(DbGroup group);
}