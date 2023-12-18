namespace Domain;

public class Comment
{
    public int Id { get; set; }
    
    public DateTime Date { get; set; }
    public string Content { get; set; }
    public User Author { get; set; }
}