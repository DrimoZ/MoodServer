using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public interface IUserGroupRepository
{
    DbUserGroup FetchById(int id);
}