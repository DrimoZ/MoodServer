using Infrastructure.EntityFramework.DbEntities;
using Microsoft.EntityFrameworkCore;

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
        return group;
    }
}