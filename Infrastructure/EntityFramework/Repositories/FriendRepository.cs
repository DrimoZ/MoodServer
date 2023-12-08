using Domain;
using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public class FriendRepository: IFriendRepository
{
    private readonly MoodContext _context;

    public FriendRepository(MoodContext context)
    {
        _context = context;
    }

    public DbFriend Create(DbFriend friend)
    {
        _context.Friends.Add(friend);
        _context.SaveChanges();
        return friend;
    }

    public bool Delete(string userId)
    {
        var friend = _context.Friends.FirstOrDefault(f => f.UserId == userId);
        if (friend == null)
            return false;
        var otherFriend = _context.Friends.FirstOrDefault(f => f.FriendId == userId);
        if (otherFriend == null)
            return false;
        _context.Friends.Remove(friend);
        _context.Friends.Remove(otherFriend);
        _context.SaveChanges();

        return true;
    }

    public IEnumerable<DbUser> FetchFriends(string userId)
    {
        var friends = _context.Friends
            .Where(f => f.UserId == userId)
            .Join(_context.Users,
                friend => friend.FriendId,
                user => user.Id,
                (friend, user) => user)
            .ToList();

        return friends;
    }
    
    public bool IsFriend(string userId, string friendId)
    {
        var friend = _context.Friends.FirstOrDefault(u => u.UserId == userId && u.FriendId == friendId);
        return friend != null;
    }
    

    public DbFriend FetchById(string userId, string otherId)
    {
        var friend = _context.Friends.FirstOrDefault(f => f.UserId == userId && f.FriendId == otherId);
        if(friend == null) throw new KeyNotFoundException($"Friend Not Found");
        
        return friend;
    }

    public int FetchFriendCount(string userId)
    {
        var count = _context.Friends
            .Count(f => f.UserId == userId);
        
        return count;
    }
}