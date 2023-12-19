using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Communications;

public class GroupRepository:IGroupRepository
{
    private readonly MoodContext _context;

    public GroupRepository(MoodContext context)
    {
        _context = context;
    }

    public DbGroup Create(DbGroup group)
    {
        _context.Groups.Add(group);
        return group;
    }

    public bool Delete(DbGroup db)
    {
        var dbGroup = _context.Groups.FirstOrDefault(grp => grp.Id == db.Id);
        if (dbGroup == null) return false;
        _context.Groups.Remove(dbGroup);
        _context.SaveChanges();
        return true;
    }

    public DbGroup FetchById(int id)
    {
        var entity = _context.Groups.FirstOrDefault(g => g.Id == id);
        if (entity == null) throw new KeyNotFoundException("Group cannot be found");
        return entity;
    }

    public bool UpdateGroup(DbGroup group)
    {
        var entity = _context.Groups.FirstOrDefault(g => g.Id == group.Id);
        if (entity == null) return false;
        entity.Name = group.Name;
        entity.ProprioId = group.ProprioId;
        _context.SaveChanges();
        return true;
    }
}