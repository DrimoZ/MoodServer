using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Users;

public interface IUserGroupRepository
{
    DbUserGroup FetchById(int id);
    DbUserGroup Create(DbUserGroup usrGrp);

    public IEnumerable<DbUserGroup> FetchAllByUserId(string userId);
    
    public IEnumerable<DbUserGroup> FetchAllByGroupId(int groupId);
    public DbUserGroup FetchByGroupIdUserId(int groupId, string id);
}