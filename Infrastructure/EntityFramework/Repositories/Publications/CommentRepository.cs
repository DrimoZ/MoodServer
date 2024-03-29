using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Publications;

public class CommentRepository: ICommentRepository
{
    private readonly MoodContext _context;

    public CommentRepository(MoodContext context)
    {
        _context = context;
    }

    public DbComment Create(DbComment comment)
    {
        comment.CommentDate = DateTime.Now;
        
        _context.Comments.Add(comment);
        _context.SaveChanges();
        return comment;
    }

    public bool Delete(int commId)
    {
        var entity = _context.Comments.FirstOrDefault(e => e.CommentId == commId);

        if (entity == null)
            return false;

        _context.Comments.Remove(entity);
        _context.SaveChanges();

        return true;
    }

    public DbComment FetchById(int commId)
    {
        var entity = _context.Comments
            .FirstOrDefault(e => e.CommentId == commId);
        
        if (entity == null) throw new KeyNotFoundException("CommentNotFound");

        return entity;
    }
    
    public IEnumerable<DbComment> FetchCommentsByPublicationId(int pubId)
    { 
        return _context.Comments
            .Where(e => e.PublicationId == pubId)
            .ToList();
    }
}