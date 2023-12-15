using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Communications;

public class MessageRepository:IMessageRepository
{
    private readonly MoodContext _context;

    public MessageRepository(MoodContext context)
    {
        _context = context;
    }

    public DbMessage Create(DbMessage message, int userGroupId)
    {
        message.UserGroupId = userGroupId;
        message.Date= DateTime.Now;
        _context.Messages.Add(message);
        return message;
    }

    public IEnumerable<DbMessage> FetchAllMessageFromUserGroup(int userGroupId)
    {
        return _context.Messages.Where(msg => msg.UserGroupId == userGroupId);
    }
}