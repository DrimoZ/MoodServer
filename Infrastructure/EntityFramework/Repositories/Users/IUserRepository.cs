using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Users;

public interface IUserRepository
{
    DbUser Create(DbUser user);
    bool Update(DbUser user);
    bool Delete(string id);
    DbUser FetchById(string id);
    DbUser FetchByLoginOrMail(string login);
    DbUser FetchByLoginAndMail(string login, string mail);
    DbUser FetchByLogin(string login);
    IEnumerable<DbUser> FetchUsersByFilter(string userIdToIgnore, string nameFilter, int userCount);
    bool CheckDuplicatedMail(string userId, string mail);
}