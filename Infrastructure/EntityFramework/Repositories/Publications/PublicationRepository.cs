using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Publications;

public class PublicationRepository:IPublicationRepository
{
    private readonly MoodContext _context;

    public PublicationRepository(MoodContext context)
    {
        _context = context;
    }
    
    public DbPublication Create(DbPublication publication)
    {
        publication.Date = DateTime.Now;

        _context.Publications.Add(publication);
        _context.SaveChanges();
        return publication;
    }

    public bool Update(DbPublication publication)
    {
        throw new NotImplementedException();
    }

    public bool UpdateDelete(int id, bool isDeleted)
    {
        var entity = FetchById(id);
        if (entity == null) throw new KeyNotFoundException("PublicationIdNotFound");
        entity.IsDeleted = true;
        _context.SaveChanges();
        return true;
    }


    public bool Delete(int id)
    {
        var entity = _context.Publications.FirstOrDefault(e => e.Id == id);

        if (entity == null)
            return false;

        _context.Publications.Remove(entity);
        _context.SaveChanges();

        return true;
    }

    public IEnumerable<DbPublication> FetchUserPublications(string userId)
    {
        return _context.Publications
            .Where(p => p.UserId == userId)
            .ToList();
    }

    public int FetchPublicationCount(string userId)
    {
        var count = _context.Publications
            .Count(p => p.UserId == userId);
        
        return count;
    }

    public DbPublication FetchById(int id)
    {
        var entity = _context.Publications
            .FirstOrDefault(pub => pub.Id == id);
        
        if (entity == null) throw new KeyNotFoundException("PublicationNotFound");

        return entity;
    }
}