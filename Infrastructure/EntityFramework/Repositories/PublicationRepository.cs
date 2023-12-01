using Domain;
using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public class PublicationRepository:IPublicationRepository
{
    private readonly MoodContext _context;

    public PublicationRepository(MoodContext context)
    {
        _context = context;
    }

    public DbPublication? Get(int id)
    {
        throw new NotImplementedException();
    }

    public DbPublication Create(DbPublication publication)
    {
        _context.Publications.Add(publication);
        _context.SaveChanges();
        return publication;
    }

    public bool Update(DbPublication publication)
    {
        throw new NotImplementedException();
    }

    public bool Delete(string id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DbPublication> FetchPublications(string userId)
    {
        return _context.Publications
            .Select(pub => pub)
            .Where(pub => pub.UserId == userId)
            .ToList();
    }
    
    public int FetchPublicationCount(string userId)
    {
        var count = _context.Publications
            .Count(p => p.UserId == userId);
        
        return count;
    }
}