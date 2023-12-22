using Microsoft.AspNetCore.Identity;

namespace Infrastructure.EntityFramework.DbEntities;

public class DbAccount
{
    public string AccountId { get; set; }
    public string? AccountPhoneNumber { get; set; }
    public string? AccountDescription { get; set; }
    public DateTime AccountBirthDate { get; set; }
    public int? ImageId { get; set; }
}