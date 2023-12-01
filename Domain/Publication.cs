namespace Domain;

public class Publication
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime date { get; set; }
    public bool isDeleted { get; set; }
    
    public List<Comment> Comments { get; set; }
    public List<Like> Likes { get; set; }
}