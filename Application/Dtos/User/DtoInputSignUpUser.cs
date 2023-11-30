using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.User;

public class DtoInputSignUpUser
{
    [Required] public string Mail { get; set; }
    [Required] public string Login { get; set; }
    [Required] public string Name { get; set; }
    [Required] public string Password { get; set; }
    [Required] public DateTime Birthdate { get; set; }
}