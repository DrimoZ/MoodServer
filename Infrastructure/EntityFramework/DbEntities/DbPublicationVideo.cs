namespace Infrastructure.EntityFramework.DbEntities;

public class DbPublicationVideo
{
    public int Id { get; set; }
    public string Extension { get; set; }
    public string Content { get; set; }
    public int IdPublication { get; set; }
}