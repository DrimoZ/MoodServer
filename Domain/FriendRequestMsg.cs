namespace Domain;

public class FriendRequestMsg : Msg
{
    public int IdFriendRequestMsg { get; set; }
    public int IdUser { get; set; }
    public int IdFriend { get; set; }
    
    public FriendRequestMsg(){}
}