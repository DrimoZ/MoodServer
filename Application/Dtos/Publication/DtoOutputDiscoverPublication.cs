namespace Application.Dtos.Publication;

public class DtoOutputDiscoverPublication
{
    public string Id { get; set; }
    public string Content { get; set; }
    public DateTime date { get; set; }
    
    public int LikeCount { get; set; }
    public int CommentCount { get; set; }

    public IEnumerable<DtoElement> Elements { get; set; }

    public class DtoElement
    {
        public int Id { get; set; }
        public string Extension { get; set; }
        public string Content { get; set; }
    }
}