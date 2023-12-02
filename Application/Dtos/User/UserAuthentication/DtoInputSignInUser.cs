using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.User.UserAuthentication;

public class DtoInputSignInUser
{
    [Required] public string Login { get; set; }
    [Required] public string Password { get; set; }
    [Required] public bool StayLoggedIn { get; set; }
}