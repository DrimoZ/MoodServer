namespace Application.Dtos.Publication;

public class DtoOutputPublicationComment
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Content { get; set; }
    public int? IdAuthorImage { get; set; }
    public string NameAuthor { get; set; }
    public string IdAuthor { get; set; }
}