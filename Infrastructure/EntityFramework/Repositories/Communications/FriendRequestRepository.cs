using System.Diagnostics.CodeAnalysis;
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

    [SuppressMessage("ReSharper.DPA", "DPA0000: DPA issues")]
    public bool IsRequestPresent(string userId, string friendId)
    {
        return _context.FriendRequests
            .Any(fr => fr.UserId == userId && fr.FriendId == friendId && !fr.IsDone);
    }

    public bool Delete(int id)
    {
        var entity = _context.FriendRequests.FirstOrDefault(e => e.Id == id);

        if (entity == null)
            return false;

        _context.FriendRequests.Remove(entity);
        _context.SaveChanges();
        
        return true;
    }
    
    public bool SetIsDone(int id, bool value)
    {
        var entity = _context.FriendRequests.FirstOrDefault(e => e.Id == id);

        if (entity == null)
            return false;

        entity.IsDone = value;
        _context.SaveChanges();

        return true;
    }
    
    public bool SetIsAccepted(int id, bool value)
    {
        var entity = _context.FriendRequests.FirstOrDefault(e => e.Id == id);

        if (entity == null)
            return false;

        entity.IsAccepted = value;
        

        return true;
    }

    public DbFriendRequest FetchRequestByIds(string userId, string friendId)
    {
        var entity = _context.FriendRequests
            .FirstOrDefault(fr => ((fr.UserId == userId && fr.FriendId == friendId) || (fr.UserId == friendId && fr.FriendId == userId)) && !fr.IsDone);
        if (entity == null) throw new KeyNotFoundException("FriendRequestNotFound");
        return entity;
    }
}