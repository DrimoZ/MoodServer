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
        throw new NotImplementedException();
    }
}