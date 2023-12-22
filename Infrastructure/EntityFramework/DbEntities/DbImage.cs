namespace Infrastructure.EntityFramework.DbEntities;

public class DbImage
{
    public int ImageId { get; set; }
    public DateTime ImageDate { get; set; }
    public byte[] ImageData { get; set; }
}