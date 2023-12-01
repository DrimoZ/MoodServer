namespace Infrastructure.EntityFramework.DbEntities;

public class DbPublication
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Content { get; set; }
    
    public DateTime Date { get; set; }
    
    public bool isDeleted { get; set; }
}