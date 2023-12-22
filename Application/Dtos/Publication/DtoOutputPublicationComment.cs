namespace Application.Dtos.Publication;

public class DtoOutputPublicationComment
{
    public int CommentId { get; set; }
    public DateTime CommentDate { get; set; }
    public string CommentContent { get; set; }
    public int? AuthorImageId { get; set; }
    public string AuthorName { get; set; }
    public string AuthorId { get; set; }
}