namespace Domain;

public class Publication
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime date { get; set; }
    
    public IEnumerable<PublicationElement> Elements { get; set; }
    public IEnumerable<Comment> Comments { get; set; }
    public IEnumerable<Like> Likes { get; set; }
}