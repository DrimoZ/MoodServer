namespace Application.Dtos.Group;

public class DtoInputCreateGroup
{
    public string Name { get; set; }
    public IEnumerable<string> UserIds { get; set; }
}