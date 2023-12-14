namespace Application.Dtos.User.UserData;

public class DtoOutputUserDiscover
{
    public int CommonFriendCount { get; set; }
    public string Id { get; set; }
    public int? IdImage { get; set; }
    public int IsFriendWithConnected { get; set; }

    public string Login { get; set; }
    public string Name { get; set; }
}