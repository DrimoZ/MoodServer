namespace Infrastructure.EntityFramework.DbEntities;

public class DbImage
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public byte[] Data { get; set; }
}