using Application.Services.Users.Util;
using Domain;

namespace Application.Services.Users;

public interface IUserService
{
    User FetchById(string id, IEnumerable<EUserFetchAttribute> attributesToFetch);
    User FetchOtherById(string id, IEnumerable<EUserFetchAttribute> attributesToFetch, bool isFriend);
}