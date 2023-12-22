namespace Domain;

public class Comment
{
    public int CommentId { get; set; }
    
    public DateTime CommentDate { get; set; }
    public string CommentContent { get; set; }
    
    public string AuthorId { get; set; }
    public string AuthorName { get; set; }
    public int? AuthorImageId { get; set; }
    public int AuthorRole { get; set; }
}