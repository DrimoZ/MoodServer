namespace Infrastructure.EntityFramework.DbEntities;

public class DbPublication
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Content { get; set; }
}