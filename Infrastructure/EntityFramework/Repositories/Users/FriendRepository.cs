using System.Diagnostics.CodeAnalysis;
using Infrastructure.EntityFramework.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories.Users;

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

    public bool Delete(string userId, string otherId)
    {
        var friend = _context.Friends.FirstOrDefault(f => f.UserId == userId && f.FriendId == otherId);
        if (friend == null)
            return false;
        var otherFriend = _context.Friends.FirstOrDefault(f =>f.UserId == otherId && f.FriendId == userId);
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
                user => user.UserId,
                (friend, user) => user)
            .Where(user => !user.IsDeleted)
            .ToList();
        
        return friends;
    }
    
    public bool IsFriend(string userId, string friendId)
    {
        return _context.Friends.Any(u => u.UserId == userId && u.FriendId == friendId);
    }
    

    public DbFriend FetchById(string userId, string otherId)
    {
        var friend = _context.Friends.FirstOrDefault(f => f.UserId == userId && f.FriendId == otherId);
        if(friend == null) throw new KeyNotFoundException($"Friend Not Found");
        
        return friend;
    }

    public int FetchFriendCount(string userId)
    {
        return _context.Friends
            .Where(f => f.UserId == userId)
            .Select(f => f.FriendId)
            .AsNoTracking()
            .Count();
    }
    
    public int FetchCommonFriendsCount(string userId, string friendId)
    {
        return _context.Friends
            .Where(f => f.UserId == userId || f.FriendId == userId)
            .GroupBy(f => f.FriendId)
            .Count(g => _context.Friends.Any(f => f.UserId == friendId && f.FriendId == g.Key));
    }
}






















