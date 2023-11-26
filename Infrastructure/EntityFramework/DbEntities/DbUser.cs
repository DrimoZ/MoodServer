namespace Infrastructure.EntityFramework.DbEntities;

public class DbUser
{
    public int Id { get; set; }
    public string Mail { get; set; }
    public string Login { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public int Role { get; set; }
    public string Title { get; set; }
    public int AccountId { get; set; }
}