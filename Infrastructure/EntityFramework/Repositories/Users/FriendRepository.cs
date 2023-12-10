using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Users;

public class FriendRepository: IFriendRepository
{
    private readonly MoodContext _context;

    public FriendRepository(MoodContext context)
    {
        _context = context;
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

    public int FetchFriendCount(string userId)
    {
        var count = _context.Friends
            .Count(f => f.UserId == userId);
        
        return count;
    }
    
    public int FetchCommonFriendsCount(string userId, string friendId)
    {
        return _context.Friends
            .Join(_context.Friends, f1 => f1.FriendId, f2 => f2.FriendId, (f1, f2) => new { f1, f2 })
            .Count(x => x.f1.UserId == userId && x.f2.UserId == friendId);
    }
}






















