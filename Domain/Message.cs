namespace Domain;

public class Message : Communication
{
    public string Content { get; set; }
    public User User { get; set; }
}