namespace Domain;

public class Account
{
    public string Id { get; set; }
    
    public string PhoneNumber { get; set; }
    public string Description { get; set; }
    
    public DateOnly BirthDate { get; set; }
}