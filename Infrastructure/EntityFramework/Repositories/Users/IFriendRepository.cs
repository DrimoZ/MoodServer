using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Users;

public interface IFriendRepository
{
    IEnumerable<DbUser> FetchFriends(string userId);
    int FetchFriendCount(string userId);
    bool IsFriend(string userId, string friendId);
}