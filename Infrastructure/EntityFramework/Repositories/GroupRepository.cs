using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

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
        _context.SaveChanges();
        return group;
    }
}