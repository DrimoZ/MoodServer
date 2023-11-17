namespace Domain;

public class Account
{
    public int IdAccount { get; set; }
    public string PhoneNumberAccount { get; set; }
    public string DescriptionAccount { get; set; }
    public DateOnly BirthDateAccount { get; set; }

    public Account()
    {
       
    }
}