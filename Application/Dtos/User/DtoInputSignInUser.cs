namespace Application.Dtos.User;

public class DtoInputSignInUser
{
    public string Login { get; set; }
    public string Password { get; set; }
    public bool StayLoggedIn { get; set; }
}