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
        _context.Messages.Add(message);
        return message;
    }
}