namespace Application.Dtos.Publication;

public class DtoOutputPublication
{
    public string Id { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }
    
    public int LikeCount { get; set; }
    public int CommentCount { get; set; }
    
    public bool IsFromConnected { get; set; }
    public bool HasConnectedLiked { get; set; }
    public string NameAuthor { get; set; }
    public string IdAuthor { get; set; }
    public int? IdAuthorImage { get; set; }

    public IEnumerable<DtoOutputElement> Elements { get; set; }
    public IEnumerable<DtoOutputComment> Comments { get; set; }
    
    
    public class DtoOutputElement
    {
        public int Id { get; set; }
        public int IdImage { get; set; }
    }
    
    public class DtoOutputComment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public int IdAuthorImage { get; set; }
        public string NameAuthor { get; set; }
        public string IdAuthor { get; set; }
    }
}