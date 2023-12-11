using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.Repositories.Images;

public class ImageRepository:IImageRepository
{
    private readonly MoodContext _context;

    public ImageRepository(MoodContext context)
    {
        _context = context;
    }

    public DbImage Create(DbImage image)
    { 
        image.Date = DateTime.Now;
        _context.Images.Add(image);
        _context.SaveChanges();
        return image;
    }

    public bool Delete(DbImage image)
    {
        throw new NotImplementedException();
    }

    public DbImage FetchById(int id)
    {
        var entity = _context.Images.FirstOrDefault(i => i.Id == id);
        if (entity == null) throw new  KeyNotFoundException($"Image not found");
        return entity;
    }
}