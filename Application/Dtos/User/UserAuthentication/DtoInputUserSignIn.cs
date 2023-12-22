using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.User.UserAuthentication;

public class DtoInputUserSignIn
{
    [Required] public string UserLogin { get; set; }
    [Required] public string UserPassword { get; set; }
    [Required] public bool StayLoggedIn { get; set; }
}