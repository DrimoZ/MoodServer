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

    public bool UpdateDelete(string id, bool isDeleted)
    {
        var entity = FetchById(id);
    }


    public bool Delete(string id)
    {
        var entity = _context.Publications.FirstOrDefault(e => e.Id == id);

        if (entity == null)
            return false;

        _context.Publications.Remove(entity);
        _context.SaveChanges();

        return true;
    }

    public List<DbPublication> FetchByIdUser(string userId)
    {
        return _context.Publications
            .Select(pub => pub)
            .Where(pub => pub.UserId == userId)
            .ToList();
    }

    public DbPublication FetchById(string id)
    {
        var entity = _context.Publications
            .FirstOrDefault(pub => pub.Id == id);
        if (entity == null) throw new KeyNotFoundException("PublicationIdNotFound");

        return entity;
    }
}