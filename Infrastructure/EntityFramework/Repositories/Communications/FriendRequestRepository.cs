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

    public bool Delete(int id)
    {
        var entity = _context.FriendRequests.FirstOrDefault(e => e.Id == id);

        if (entity == null)
            return false;

        _context.FriendRequests.Remove(entity);

        return true;
    }

    public DbFriendRequest FetchRequestByIds(string userId, string friendId)
    {
        var entity = _context.FriendRequests.FirstOrDefault(fr => fr.UserId == userId && fr.FriendId == friendId);
        if (entity == null) throw new KeyNotFoundException("FriendRequestNotFound");
        return entity;
    }
}