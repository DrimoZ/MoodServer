namespace Domain;

public class Comment
{
    public int Id { get; set; }
    
    public DateTime Date { get; set; }
    public string Content { get; set; }
    
    public string IdAuthor { get; set; }
    public string NameAuthor { get; set; }
    public int? IdAuthorImage { get; set; }
}