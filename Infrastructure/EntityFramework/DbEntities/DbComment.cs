namespace Infrastructure.EntityFramework.DbEntities;

public class DbComment
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Content { get; set; }
    
    public bool IsDeleted { get; set; }
    
    public int PublicationId { get; set; }
    public string UserId { get; set; }
}