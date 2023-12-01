using Microsoft.AspNetCore.Identity;

namespace Infrastructure.EntityFramework.DbEntities;

public class DbAccount
{
    public string Id { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Description { get; set; }
    public DateTime BirthDate { get; set; }
}