namespace Domain;

public class FriendRequest
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public User User { get; set; }
    public User Friend { get; set; }
}