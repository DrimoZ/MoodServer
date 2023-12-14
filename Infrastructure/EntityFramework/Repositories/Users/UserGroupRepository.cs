using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Users;

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

    public DbUserGroup Create(DbUserGroup usrGrp)
    {
        _context.UserGroups.Add(usrGrp);
        return usrGrp;
    }

    public IEnumerable<DbUserGroup> FetchAllByUserId(string userId)
    {
        var groups =_context.UserGroups.Where(userGroup => userGroup.UserId == userId);
        return groups;
    }

    public IEnumerable<DbUserGroup> FetchAllByGroupId(int groupId)
    {
        return _context.UserGroups.Where(userGroup => userGroup.GroupId == groupId);
    }
}