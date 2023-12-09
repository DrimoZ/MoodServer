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
}