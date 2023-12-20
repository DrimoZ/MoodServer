namespace Application.Dtos.Group;

public class DtoOutputGroup
{
    public int Id { get; set; }
    public string? Name { get; set; }
    
    public bool IsPrivate { get; set; }
    
    public string? ProprioId { get; set; }
}