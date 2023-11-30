namespace Domain;

public class Account
{
    public string Id { get; set; }
    
    public string PhoneNumber { get; set; }
    public string Description { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    
    public override string ToString()
    {
        return $"Account: Id={Id}, PhoneNumber={PhoneNumber}, Description={Description}, BirthDate={BirthDate}";
    }
}