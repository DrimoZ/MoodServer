using System.Text.RegularExpressions;

namespace Application.Services.Utils;

public class BCryptService
{
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
    
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
}

