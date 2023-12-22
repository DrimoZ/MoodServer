namespace Infrastructure.EntityFramework.DbEntities;

public class DbComment
{
    public int CommentId { get; set; }
    public DateTime CommentDate { get; set; }
    public string CommentContent { get; set; }
    public int PublicationId { get; set; }
    public string UserId { get; set; }
}