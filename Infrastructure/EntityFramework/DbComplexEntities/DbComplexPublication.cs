using Domain;
using Infrastructure.EntityFramework.DbEntities;

namespace Infrastructure.EntityFramework.DbComplexEntities;

public class DbComplexPublication
{
    public int PublicationId { get; set; }
    public string UserId { get; set; }
    public string PublicationContent { get; set; }
    
    public DateTime PublicationDate { get; set; }
    
    public bool IsDeleted { get; set; }
    
    public List<DbPublicationElement> Elements { get; set; }
}