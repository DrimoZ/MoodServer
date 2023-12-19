using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.User.UserData;

public class DtoInputUpdateUser
{
        [Required]
        public string Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string? Title { get; set; }
        
        [Required]
        public string Mail { get; set; }
        
        [Required]
        public DateTime Birthdate { get; set; }
        
        [Required]
        public string Description { get; set; }
}