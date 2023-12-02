namespace Infrastructure.EntityFramework.DbEntities;

public class DbGroup
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
}