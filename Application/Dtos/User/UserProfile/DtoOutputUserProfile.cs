namespace Application.Dtos.User.UserProfile;

public class DtoOutputUserProfile
{
    public string AccountDescription { get; set; }
    
    public int FriendCount { get; set; }
    
    public int? ImageId { get; set; }

    public bool IsConnectedUser { get; set; }

    public int IsFriendWithConnected { get; set; }

    public string UserName { get; set; }

    public int PublicationCount { get; set; }
    
    public int UserRole { get; set; }
}