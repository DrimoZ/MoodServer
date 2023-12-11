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

    public DbGroup FetchById(int id)
    {
        var entity = _context.Groups.FirstOrDefault(g => g.Id == id);
        if (entity == null) throw new KeyNotFoundException("Group cannot be found");
        return entity;
    }
}