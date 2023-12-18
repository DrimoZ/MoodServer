namespace Infrastructure.EntityFramework.DbEntities;

public class DbComment
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Content { get; set; }
    public int IdPublication { get; set; }
    public string IdUser { get; set; }
}