namespace Infrastructure.EntityFramework.DbEntities;

public class DbPublicationPhoto
{
    public int Id { get; set; }
    public string Extension { get; set; }
    public string Content { get; set; }
    public int IdPublication { get; set; }
}