namespace Application.Dtos.User.User;

public class DtoOutputNotification
{
    public DateTime FriendRequestDate { get; set; }
    public int? ImageId { get; set; }
    public bool IsAccepted { get; set; }
    public bool IsConnectedEmitter { get; set; }
    public bool IsDone { get; set; }
    public int IsFriendWithConnected { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
}