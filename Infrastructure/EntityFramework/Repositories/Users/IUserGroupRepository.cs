using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Users;

public interface IUserGroupRepository
{
    DbUserGroup FetchById(int id);
    DbUserGroup Create(DbUserGroup usrGrp);
}