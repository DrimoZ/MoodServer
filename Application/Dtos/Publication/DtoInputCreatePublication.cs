using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Publication;

public class DtoInputCreatePublication
{
    [Required] public string Content { get; set; }
    [Required] public string UserId { get; set; }
}