namespace Application.Dtos.User;

public class DtoInputSignIn
{
    public string Login { get; set; }
    public string Password { get; set; }
    public bool StayLoggedIn { get; set; }
}