namespace Application.Dtos.User.UserProfile;

public class DtoOutputUserFriends
{
    public bool IsConnectedUser { get; set; }
    public bool IsFriendPublic { get; set; }
    
    public IEnumerable<DtoFriend> Friends { get; set; }
    
    public class DtoFriend
    {
        public int CommonFriendCount { get; set; }
        public string Id { get; set; }
        public bool IsFriendWithConnected { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
    }
}