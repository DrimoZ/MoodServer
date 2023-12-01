using Domain;
using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public interface IFriendRepository
{
    IEnumerable<DbUser> FetchFriends(string userId);
    int FetchFriendCount(string userId);
}