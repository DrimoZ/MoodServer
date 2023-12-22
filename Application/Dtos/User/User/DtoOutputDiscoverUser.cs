namespace Application.Dtos.User.User;

public class DtoOutputDiscoverUser
{
    public int CommonFriendCount { get; set; }
    public string UserId { get; set; }
    public int? ImageId { get; set; }
    public int IsFriendWithConnected { get; set; }
    public string UserName { get; set; }
}