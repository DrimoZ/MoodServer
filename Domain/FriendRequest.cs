namespace Domain;

public class FriendRequest : Communication
{
    public User User { get; set; }
    public User Friend { get; set; }
}