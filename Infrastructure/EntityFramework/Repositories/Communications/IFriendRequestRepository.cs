using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Communications;

public interface IFriendRequestRepository
{
    public DbFriendRequest Create (DbFriendRequest friendRequest);
    public bool IsRequestPresent (string userId, string friendId);
    bool Delete (int id);
    bool SetIsDone (int id, bool value);
    bool SetIsAccepted (int id, bool value);
    DbFriendRequest FetchRequestByIds (string userId, string friendId);
}