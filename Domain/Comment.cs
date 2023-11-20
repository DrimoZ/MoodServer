namespace Domain;

public class Comment
{
    public int IdComment { get; set; }
    public DateTime SendDateComment { get; set; }
    public string ContentComment { get; set; }
    public int IdPub { get; set; }
    public int IdAccount { get; set; }
    
    public Comment(){}
}