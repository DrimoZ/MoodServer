namespace Infrastructure.EntityFramework.DbEntities;

public class DbFriendRequest
{
    public int FriendRequestId { get; set; }
    public DateTime FriendRequestDate { get; set; }
    public bool IsDone { get; set; }
    public bool IsAccepted { get; set; }
    public string UserId { get; set; }
    public string FriendId { get; set; } 
}