namespace Domain;
public class User
{
    public int IdUser { get; set; }
    public int IdAccount { get; set; }
    public int RoleUser { get; set; }
    public string LoginUser { get; set; }
    public string NameUser { get; set; }
    
    public string MailUser { get; set; }
    public string PasswordUser { get; set; }
    public string TitreUser { get; set; }

    public User()
    {
        
    }
}
