using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public class UserGroupRepository:IUserGroupRepository
{
    private readonly MoodContext _context;

    public UserGroupRepository(MoodContext context)
    {
        _context = context;
    }

    public DbUserGroup FetchById(int id)
    {
        var usergroup = _context.UserGroups.FirstOrDefault(u => u.Id == id);
        if (usergroup == null) throw new KeyNotFoundException("userGroupNotFound");
        return usergroup;
    }
}