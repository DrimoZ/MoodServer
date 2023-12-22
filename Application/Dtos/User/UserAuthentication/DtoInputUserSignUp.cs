using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Application.Dtos.User.UserAuthentication;

public class DtoInputUserSignUp
{
    [Required]
    public string UserName
    {
        get => _userName;
        set
        {
            if (value.Length > 128) throw new ArgumentException("The Name must contains less than 128 characters.");
            _userName = value;
        }
    }
    private string _userName;
    
    [Required] 
    [MinLength(8, ErrorMessage = "The login must be at least 8 characters long.")]
    public string UserLogin { get; set; }
    

    [Required] 
    public string UserMail 
    { 
        get => _userMail;
        set
        {
            if (!IsValidEmail(value)) throw new ArgumentException("Invalid Email Format");
            _userMail = value;
        } 
    }
    private string _userMail;
    
    [Required]
    [MinLength(8, ErrorMessage = "The password should not be less than 8 characters.")]
    [MaxLength(32, ErrorMessage = "The password should be less than 32 characters.")]

    public string UserPassword
    {
        get => _userPassword;
        set
        {
            if (!ValidatePassword(value)) throw new ArgumentException("Invalid Password Format");
            _userPassword = value;
        }
    }
    private string _userPassword;
    
    [Required] 
    public DateTime AccountBirthdate 
    { 
        get => _accountBirthdate;
        set 
        { 
            if (value.Date > DateTime.Today.AddYears(-13))
            {
                throw new ArgumentException("The birthdate must be at least 13 years ago.");
            }
            _accountBirthdate = value; 
        } 
    }
    private DateTime _accountBirthdate;
    
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
    
    private static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
        }
        catch
        {
            throw new ArgumentException("Invalid Email Format");
        }
    }
}