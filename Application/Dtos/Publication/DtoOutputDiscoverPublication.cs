namespace Application.Dtos.Publication;

public class DtoOutputDiscoverPublication
{
    public string PublicationId { get; set; }
    public string PublicationContent { get; set; }
    public DateTime PublicationDate { get; set; }
    
    public int LikeCount { get; set; }
    public int CommentCount { get; set; }

    public IEnumerable<DtoElement> Elements { get; set; }

    public class DtoElement
    {
        public int ElementId { get; set; }
        public int ImageId { get; set; }
    }
}