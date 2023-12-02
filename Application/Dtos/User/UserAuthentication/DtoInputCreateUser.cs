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
    public DtoAccount Account { get; set; }
    
    public class DtoAccount
    {
        [Required] public string Id { get; set; }
        [JsonIgnore] public string PhoneNumber { get; set; }
        [JsonIgnore] public string Description { get; set; }
        [Required] public DateTime BirthDate { get; set; }
    }
}