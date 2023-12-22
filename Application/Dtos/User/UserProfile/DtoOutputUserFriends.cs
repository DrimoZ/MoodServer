namespace Application.Dtos.User.UserProfile;

public class DtoOutputUserFriends
{
    public bool IsConnectedUser { get; set; }
    public bool IsFriendPublic { get; set; }
    
    public IEnumerable<DtoFriend> Friends { get; set; }
    
    public class DtoFriend
    {
        public int CommonFriendCount { get; set; }
        public string UserId { get; set; }
        public int? ImageId { get; set; }
        public int IsFriendWithConnected { get; set; }
        public string UserLogin { get; set; }
        public string UserName { get; set; }
    }
}