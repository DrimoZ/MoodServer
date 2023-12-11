namespace Infrastructure.EntityFramework.DbEntities;

public class DbLike
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    
    public int Type { get; set; }
    
    public int PublicationId { get; set; }
    public string UserId { get; set; }
}