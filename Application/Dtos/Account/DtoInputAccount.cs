namespace Application.Dtos.Account;

public class DtoInputAccount
{
    public int Id { get; set; }
    public string PhoneNumber { get; set; }
    public string Description { get; set; }
    public DateOnly BirthDate { get; set; }
}