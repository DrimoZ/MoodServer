using Domain;
using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public interface IFriendRepository
{
    List<DbUser> FetchFriends(string userId);
}