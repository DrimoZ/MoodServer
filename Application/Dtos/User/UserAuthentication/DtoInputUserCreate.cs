using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Dtos.User.UserAuthentication;

public class DtoInputUserCreate
{
    public string UserMail { get; set; }
    public string UserLogin { get; set; }
    public string UserName { get; set; }
    public string UserPassword { get; set; }
    public string AccountId { get; set; }
    
}