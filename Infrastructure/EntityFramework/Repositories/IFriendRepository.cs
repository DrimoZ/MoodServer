using Domain;
using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public interface IFriendRepository
{
    DbFriend Create(DbFriend friend);
    bool Delete(string userId);
    IEnumerable<DbUser> FetchFriends(string userId);
    int FetchFriendCount(string userId);
    bool IsFriend(string userId, string friendId);
    DbFriend FetchById(string userId, string otherId);
}