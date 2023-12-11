namespace Application.Dtos.Publication;

public class DtoOutputPublication
{
    public string Id { get; set; }
    public string Content { get; set; }
    public DateTime date { get; set; }
    
    public int LikeCount { get; set; }
    public int CommentCount { get; set; }

    public IEnumerable<DtoElements> Elements { get; set; }

   public class DtoElements
   {
       public int Id { get; set; }
       public string Extension { get; set; }
       public string Content { get; set; }
   }
}