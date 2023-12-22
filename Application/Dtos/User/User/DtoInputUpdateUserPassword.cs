using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Application.Dtos.User.User;

public class DtoInputUpdateUserPassword
{
    
    [Required]
    [MinLength(8, ErrorMessage = "The old password should not be less than 8 characters.")]
    [MaxLength(32, ErrorMessage = "The old password should be less than 32 characters.")]
    public string OldPassword
    {
        get => _oldPassword;
        set
        {
            if (!ValidatePassword(value)) throw new ArgumentException("Invalid Password Format");
            _oldPassword = value;
        }
    }
    private string _oldPassword;
    
    
    [Required]
    [MinLength(8, ErrorMessage = "The new password should not be less than 8 characters.")]
    [MaxLength(32, ErrorMessage = "The new password should be less than 32 characters.")]
    public string NewPassword
    {
        get => _newPassword;
        set
        {
            if (!ValidatePassword(value)) throw new ArgumentException("Invalid Password Format");
            _newPassword = value;
        }
    }
    private string _newPassword;
    
    
    private static bool ValidatePassword(string password)
    {
        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasLowerChar = new Regex(@"[a-z]+");
        var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        if (!hasLowerChar.IsMatch(password))
        {
            throw new ArgumentException("The password should contain at least one lower case letter.");
        }
        if (!hasUpperChar.IsMatch(password))
        {
            throw new ArgumentException("The password should contain at least one upper case letter.");
        }
        if (!hasNumber.IsMatch(password))
        {
            throw new ArgumentException("The password should contain at least one numeric value.");
        }
        if (!hasSymbols.IsMatch(password))
        {
            throw new ArgumentException("The password should contain at least one special case characters.");
        }
        return true;
    }
}