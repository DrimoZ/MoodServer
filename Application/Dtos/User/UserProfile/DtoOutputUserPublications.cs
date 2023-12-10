namespace Application.Dtos.User.UserProfile;

public class DtoOutputUserPublications
{
    public bool IsConnectedUser { get; set; }
    public bool IsPublicationsPublic { get; set; }
    
    public IEnumerable<DtoPublication> Publications { get; set; }
    
    public class DtoPublication
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime date { get; set; }
    
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }

        public IEnumerable<DtoElement> Elements { get; set; }

        public class DtoElement
        {
            public int Id { get; set; }
            public string Extension { get; set; }
            public string Content { get; set; }
        }
    }
}