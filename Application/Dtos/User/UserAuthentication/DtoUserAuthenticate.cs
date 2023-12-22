namespace Application.Dtos.User.UserAuthentication;

public class DtoUserAuthenticate
{
    public string UserId { get; set; }
    public string UserPassword { get; set; }
    public int UserRole { get; set; }
}