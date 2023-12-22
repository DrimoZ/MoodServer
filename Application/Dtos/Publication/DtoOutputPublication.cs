namespace Application.Dtos.Publication;

public class DtoOutputPublication
{
    public string PublicationId { get; set; }
    public string PublicationContent { get; set; }
    public DateTime PublicationDate { get; set; }
    
    public int LikeCount { get; set; }
    public int CommentCount { get; set; }
    
    public bool IsFromConnected { get; set; }
    public bool HasConnectedLiked { get; set; }
    public string AuthorName { get; set; }
    public string AuthorId { get; set; }
    public int? AuthorImageId { get; set; }
    public int AuthorRole { get; set; }

    public IEnumerable<DtoOutputElement> Elements { get; set; }
    public IEnumerable<DtoOutputComment> Comments { get; set; }
    
    
    public class DtoOutputElement
    {
        public int ElementId { get; set; }
        public int ImageId { get; set; }
    }
    
    public class DtoOutputComment
    {
        public int CommentId { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentContent { get; set; }
        public int AuthorImageId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorId { get; set; }
        public int AuthorRole { get; set; }
    }
}