namespace Infrastructure.EntityFramework.DbEntities;

public class DbUser
{
    public string UserId { get; set; }
    public string UserMail { get; set; }
    public string UserLogin { get; set; }
    public string UserName { get; set; }
    public string UserPassword { get; set; }
    public int UserRole { get; set; }
    public string? UserTitle { get; set; }
    public string AccountId { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsPublic { get; set; }
    public bool IsFriendPublic { get; set; }
    public bool IsPublicationPublic { get; set; }
}