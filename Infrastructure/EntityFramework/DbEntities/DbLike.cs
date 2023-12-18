namespace Infrastructure.EntityFramework.DbEntities;

public class DbLike
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    
    public int IdPublication { get; set; }
    public string IdUser { get; set; }
}