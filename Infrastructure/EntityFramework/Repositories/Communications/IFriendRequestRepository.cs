using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Communications;

public interface IFriendRequestRepository
{
    public DbFriendRequest Create (DbFriendRequest friendRequest);
    public bool IsRequestPresent (string userId, string friendId);
    bool Delete (int id);
    DbFriendRequest FetchRequestByIds (string userId, string friendId);
}