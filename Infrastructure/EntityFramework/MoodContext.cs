using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework;

public class MoodContext: DbContext
{
    public MoodContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}