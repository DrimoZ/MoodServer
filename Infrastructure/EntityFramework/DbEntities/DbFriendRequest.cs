namespace Infrastructure.EntityFramework.DbEntities;

public class DbFriendRequest
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string UserId { get; set; }
    public string FriendId { get; set; } 
}