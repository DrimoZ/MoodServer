using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Account;

public class DtoInputCreateAccount
{
    [Required] public DateTime BirthDate { get; set; }
}