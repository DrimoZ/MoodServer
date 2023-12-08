using Domain;
using Infrastructure.EntityFramework.DbComplexEntities;
using Infrastructure.EntityFramework.DbEntities;
using Microsoft.EntityFrameworkCore.Internal;

namespace Infrastructure.EntityFramework.Repositories;

public class PublicationRepository:IPublicationRepository
{
    private readonly MoodContext _context;

    public PublicationRepository(MoodContext context)
    {
        _context = context;
    }
    
    public DbComplexPublication Create(DbComplexPublication publication)
    {
        publication.Date = DateTime.Now;
        _context.Publications.Add(publication);
        _context.SaveChanges();
        return publication;
    }

    public bool Update(DbComplexPublication publication)
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

    public IEnumerable<DbComplexPublication> FetchPublications(string userId)
    {
        
    }

    public IEnumerable<DbComplexPublication> FetchFriendPublications(string userId)
    {
        
    }

    public int FetchPublicationCount(string userId)
    {
        var count = _context.Publications
            .Count(p => p.UserId == userId);
        return count;
    }

    public DbComplexPublication FetchById(int id)
    {
        var entity = _context.Publications
            .FirstOrDefault(pub => pub.Id == id);
        if (entity == null) throw new KeyNotFoundException("PublicationIdNotFound");

        return entity;
    }
}