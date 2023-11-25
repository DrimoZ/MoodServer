namespace Domain;

public class VideoPublication: Publication
{
    public int IdVideoPublication{ get; set; }
    public string Extention { get; set; }
    public string Content { get; set; }
    public VideoPublication(){}
}