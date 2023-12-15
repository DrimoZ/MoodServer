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
    
    public DbUserGroup FetchByGroupIdUserId(int groupId, string userId)
    { Console.WriteLine(userId + " " + groupId);
       var entity = _context.UserGroups.FirstOrDefault(userGroup =>
            userGroup.GroupId == groupId && userGroup.UserId == userId);
       if (entity == null) throw new KeyNotFoundException("UserGroup cannot be found");
       return entity;
    }
}