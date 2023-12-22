using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Application.Dtos.User.UserProfile;

public class DtoInputUserUpdateProfile
{
        [Required]
        public string UserId { get; set; }
        
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
        
        public string? UserTitle
        {
                get => _userTitle;
                set
                {
                        if (value == null) return;
                        if (value.Length > 32) throw new ArgumentException("The Title must contains less than 32 characters.");
                        _userTitle = value;
                }
        }
        private string _userTitle;
        
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
        public DateTime AccountBirthdate 
        { 
                get => _accountBirthdate;
                set 
                { 
                        if (value.Date > DateTime.Today.AddYears(-13)) throw new ArgumentException("The birthdate must be at least 13 years ago.");
                        _accountBirthdate = value; 
                } 
        }
        private DateTime _accountBirthdate;
        
        public string? AccountDescription
        {
                get => _accountDescription;
                set
                {
                        if (value == null ) return;
                        if (value.Length > 256) throw new ArgumentException("The description must contains less than 256 characters.");
                        _accountDescription = value;
                }
        }
        private string _accountDescription;
        
        
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