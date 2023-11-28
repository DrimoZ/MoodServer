namespace Infrastructure.EntityFramework.DbEntities;

public class DbAccount
{
    public int Id { get; set; }
    public string PhoneNumber { get; set; }
    public string Description { get; set; }
    public DateTime BirthDate { get; set; }
}