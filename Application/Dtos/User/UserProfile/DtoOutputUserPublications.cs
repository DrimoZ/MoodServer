namespace Application.Dtos.User.UserProfile;

public class DtoOutputUserPublications
{
    public bool IsConnectedUser { get; set; }
    public bool IsPublicationsPublic { get; set; }
    
    public IEnumerable<DtoPublication> Publications { get; set; }
    
    public class DtoPublication
    {
        public string PublicationId { get; set; }
        public string PublicationContent { get; set; }
        public DateTime PublicationDate { get; set; }
    
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }

        public IEnumerable<DtoPublicationElement> Elements { get; set; }

        public class DtoPublicationElement
        {
            public int ElementId { get; set; }
            public int ImageId { get; set; }
        }
    }
}