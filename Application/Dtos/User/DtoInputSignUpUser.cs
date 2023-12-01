using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Application.Dtos.User;

public class DtoInputSignUpUser
{
    
    [Required] 
    public string Name { get; set; }
    
    [Required] 
    [MinLength(8, ErrorMessage = "Login must be at least 8 characters long.")]
    public string Login { get; set; }
    

    [Required] 
    public string Mail 
    { 
        get => _mail;
        set
        {
            if (IsValidEmail(value))
            {
                _mail = value;
            }
        } 
    }
    private string _mail;
    
    [Required]
    public string Password
    {
        get => _password;
        set
        {
            if (ValidatePassword(value))
            {
                _password = value;
            }
        }
    }
    private string _password;
    
    [Required] 
    public DateTime Birthdate 
    { 
        get => _birthdate;
        set 
        { 
            if (value.Date > DateTime.Today.AddYears(-13))
            {
                throw new ArgumentException("Birthdate must be at least 13 years ago.");
            }
            _birthdate = value; 
        } 
    }
    private DateTime _birthdate;
    
    private static bool ValidatePassword(string password)
    {
        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasLowerChar = new Regex(@"[a-z]+");
        var hasMinimum8Chars = new Regex(@".{8,}");
        var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        if (!hasLowerChar.IsMatch(password))
        {
            throw new ArgumentException("Password should contain at least one lower case letter.");
        }
        if (!hasUpperChar.IsMatch(password))
        {
            throw new ArgumentException("Password should contain at least one upper case letter.");
        }
        if (!hasNumber.IsMatch(password))
        {
            throw new ArgumentException("Password should contain at least one numeric value.");
        }
        if (!hasSymbols.IsMatch(password))
        {
            throw new ArgumentException("Password should contain at least one special case characters.");
        }
        if (!hasMinimum8Chars.IsMatch(password))
        {
            throw new ArgumentException("Password should not be less than 8 characters.");
        }
        return true;
    }
    
    private bool IsValidEmail(string email)
    {
        try
        {
            var emailAddress = new MailAddress(email);
            return true;
        }
        catch
        {
            throw new ArgumentException("Invalid Email Format");
        }
    }
}