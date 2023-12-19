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

    public IEnumerable<DbMessage> FetchMessageGroup(int groupId, int showCount)
    {
        var lastMessages = _context.Messages
            .Join(
                _context.UserGroups,
                message => message.UserGroupId,
                userGroup => userGroup.Id,
                (message, userGroup) => new { Message = message, UserGroup = userGroup }
            )
            .Where(x => x.UserGroup.GroupId == groupId)
            .OrderByDescending(x => x.Message.Date)
            .Take(showCount)
            .Select(x => x.Message)
            .ToList();
        return lastMessages;
    }
}