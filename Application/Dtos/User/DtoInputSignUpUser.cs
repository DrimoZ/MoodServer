namespace Application.Dtos.User;

public class DtoInputSignUpUser
{
    public string Mail { get; set; }
    public string Login { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public DateTime Birthdate { get; set; }
}