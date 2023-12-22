namespace Infrastructure.EntityFramework.DbEntities;

public class DbLike
{
    public int LikeId { get; set; }
    public DateTime LikeDate { get; set; }
    
    public int PublicationId { get; set; }
    public string UserId { get; set; }
}