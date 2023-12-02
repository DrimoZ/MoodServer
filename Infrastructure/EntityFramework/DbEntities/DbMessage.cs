namespace Infrastructure.EntityFramework.DbEntities;

public class DbMessage
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int UserGroupId { get; set; }
    public int CommId { get; set; }
}