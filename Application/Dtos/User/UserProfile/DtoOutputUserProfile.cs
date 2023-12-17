namespace Application.Dtos.User.UserProfile;

public class DtoOutputUserProfile
{
    public string Description { get; set; }
    public int FriendCount { get; set; }
    public bool IsConnectedUser { get; set; }
    public bool IsPublic { get; set; }
    public int IsFriendWithConnected { get; set; }
    
    public string Login { get; set; }
    public string Name { get; set; }
    public int PublicationCount { get; set; }
    public string Title { get; set; }
    
    public int? IdImage { get; set; }
}