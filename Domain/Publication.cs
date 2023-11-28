namespace Domain;

public class Publication
{
    public int Id { get; set; }
    public string Content { get; set; }
    
    public List<Comment> Comments { get; set; }
    public List<Like> Likes { get; set; }
}