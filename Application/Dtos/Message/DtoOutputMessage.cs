namespace Application.Dtos.Message;

public class DtoOutputMessage
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string UserLogin { get; set; }
    public string UserName { get; set; }
    public string UserId { get; set; }
    public int GroupId { get; set; }
    public int CommId { get; set; }
    public DateTime Date { get; set; }
    public int? ImageId { get; set; }
    public bool IsDeleted { get; set; }
    
    public bool HasLeft { get; set; }

}