using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Publication;

public class DtoInputCreatePublication
{
    [Required] public string Content { get; set; }
    [Required] public string UserId { get; set; }

    public IEnumerable<DtoElement> Elements { get; set; }
    
    public class DtoElement
    {
        [Required] public string Extension { get; set; }
        [Required] public string Content { get; set; }
    }
}