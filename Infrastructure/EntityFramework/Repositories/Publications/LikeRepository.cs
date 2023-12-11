using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Publications;

public class LikeRepository: ILikeRepository
{
    private readonly MoodContext _context;

    public LikeRepository(MoodContext context)
    {
        _context = context;
    }

    public DbLike Create(DbLike like)
    {
        _context.Likes.Add(like);
        _context.SaveChanges();
        return like;
    }

    public bool Delete(int likeId)
    {
        var entity = _context.Likes.FirstOrDefault(e => e.Id == likeId);

        if (entity == null)
            return false;

        _context.Likes.Remove(entity);
        _context.SaveChanges();

        return true;
    }

    public DbLike FetchById(int likeId)
    {
        var entity = _context.Likes
            .FirstOrDefault(e => e.Id == likeId);
        
        if (entity == null) throw new KeyNotFoundException("LikeNotFound");

        return entity;
    }
    
    public IEnumerable<DbLike> FetchLikesByPublicationId(int pubId)
    { 
        return _context.Likes
            .Where(e => e.PublicationId == pubId)
            .ToList();
    }
    
    public int FetchLikeCountByPublicationId(int pubId)
    {
        var count = _context.Likes
            .Count(l => l.PublicationId == pubId);
        
        return count;
    }
}