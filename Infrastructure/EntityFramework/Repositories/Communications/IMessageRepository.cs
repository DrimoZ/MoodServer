using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Communications;

public interface IMessageRepository
{
    public DbMessage Create(DbMessage message, int userGroupId);
    public IEnumerable<DbMessage> FetchAllMessageFromUserGroup(int userGroupId);
    public IEnumerable<DbMessage> FetchMessageGroup(int groupId, int showCount);
    bool Delete(int id);
}