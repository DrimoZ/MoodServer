using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Communications;

public class FriendRequestRepository:IFriendRequestRepository
{
    private readonly MoodContext _context;

    public FriendRequestRepository(MoodContext context)
    {
        _context = context;
    }

    public DbFriendRequest Create(DbFriendRequest friendRequest)
    {
        friendRequest.Date = DateTime.Now;
        _context.FriendRequests.Add(friendRequest);
        _context.SaveChanges();
        
        return friendRequest;
    }

    public bool IsRequestPresent(string userId, string friendId)
    {
        return _context.FriendRequests.Any(fr => fr.UserId == userId && fr.FriendId == friendId);
    }
}