namespace Application.Dtos.Group;

public class DtoInputCreateGroup
{
    public string Name { get; set; }
    public IEnumerable<string> userIds { get; set; }
}