namespace Infrastructure.EntityFramework.DbEntities;

public class DbPublication
{
    public int PublicationId { get; set; }
    public string UserId { get; set; }
    public string PublicationContent { get; set; }
    
    public DateTime PublicationDate { get; set; }
    
    public bool IsDeleted { get; set; }
}