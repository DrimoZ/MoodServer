using Application.Services.Users.Util;
using Domain;

namespace Application.Services.Users;

public interface IUserService
{
    User FetchById(string id, IEnumerable<UserFetchAttribute> attributesToFetch);
}