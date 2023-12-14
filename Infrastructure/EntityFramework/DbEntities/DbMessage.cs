namespace Infrastructure.EntityFramework.DbEntities;

public class DbMessage
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int UserGroupId { get; set; }
    public DateTime Date { get; set; }
    public bool IsDeleted { get; set; }
}