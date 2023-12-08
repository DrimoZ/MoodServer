using Domain;

namespace Infrastructure.EntityFramework.DbComplexEntities;

public class DbComplexPublication
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Content { get; set; }
    
    public DateTime Date { get; set; }
    
    public bool IsDeleted { get; set; }
    
    public List<PhotoPublication> Photos { get; set; }
}