namespace Domain;

public class TxtMsg : Msg
{
    public int IdTxtMsg { get; set; }
    public string ContentTxtMsg { get; set; }
    public int IdUserGroup { get; set; }
    
    public TxtMsg(){}
}