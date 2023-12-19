using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Application.Dtos.User.UserData;

public class DtoInputUpdateUser
{
        [Required]
        public string Id { get; set; }
        
        [Required]
        public string Name
        {
                get => _name;
                set
                {
                        if (value.Length > 128) throw new ArgumentException("The Name must contains less than 128 characters.");
                        _name = value;
                }
        }
        private string _name;
        
        public string? Title
        {
                get => _title;
                set
                {
                        if (value.Length > 32) throw new ArgumentException("The Title must contains less than 32 characters.");
                        _title = value;
                }
        }
        private string _title;
        
        [Required] 
        public string Mail 
        { 
                get => _mail;
                set
                {
                        if (!IsValidEmail(value)) throw new ArgumentException("Invalid Email Format");
                        _mail = value;
                } 
        }
        private string _mail;
        
        [Required] 
        public DateTime Birthdate 
        { 
                get => _birthdate;
                set 
                { 
                        if (value.Date > DateTime.Today.AddYears(-13)) throw new ArgumentException("The birthdate must be at least 13 years ago.");
                        _birthdate = value; 
                } 
        }
        private DateTime _birthdate;
        
        public string Description
        {
                get => _description;
                set
                {
                        if (value.Length > 256) throw new ArgumentException("The description must contains less than 256 characters.");
                        _description = value;
                }
        }
        private string _description;
        
        
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