namespace Domain;

public class Message
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Content { get; set; }
    public User User { get; set; }
}