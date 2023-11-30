namespace Infrastructure.EntityFramework.DbEntities;

public class DbPublication
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string Content { get; set; }
    
    public DateTime date { get; set; }
}