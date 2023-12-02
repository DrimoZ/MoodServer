using System.Text.RegularExpressions;

namespace Infrastructure.EntityFramework.DbEntities;

public class dbUserGroup
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int GroupId { get; set; }
}