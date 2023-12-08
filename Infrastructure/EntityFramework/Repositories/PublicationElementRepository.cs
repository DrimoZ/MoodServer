using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories;

public class PublicationElementRepository: IPublicationElementRepository
{
    private readonly MoodContext _context;

    public PublicationElementRepository(MoodContext context)
    {
        _context = context;
    }

    public DbPublicationElement Create(DbPublicationElement pubElement)
    {
        _context.PublicationElements.Add(pubElement);
        _context.SaveChanges();
        return pubElement;
    }

    public bool Delete(int id)
    {
        var entity = _context.PublicationElements.FirstOrDefault(e => e.Id == id);

        if (entity == null)
            return false;

        _context.PublicationElements.Remove(entity);
        _context.SaveChanges();

        return true;
    }

    public DbPublicationElement FetchById(int elementId)
    {
        var entity = _context.PublicationElements
            .FirstOrDefault(e => e.Id == elementId);
        
        if (entity == null) throw new KeyNotFoundException("PublicationElementNotFound");

        return entity;
    }
    
    public IEnumerable<DbPublicationElement> FetchElementsByPublicationId(int pubId)
    { 
        return _context.PublicationElements
            .Where(e => e.IdPublication == pubId)
            .ToList();
    }
}