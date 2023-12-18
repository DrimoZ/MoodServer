using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Dtos.User.UserAuthentication;

public class DtoInputCreateUser
{
    [Required] 
    public string Mail { get; set; }
    
    [Required] 
    public string Login { get; set; }
    
    [Required] 
    public string Name { get; set; }
    
    [Required] 
    public string Password { get; set; }
    
    [Required] 
    public string AccountId { get; set; }
    
}