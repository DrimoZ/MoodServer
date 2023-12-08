namespace Application.Dtos.Publication;

public class DtoOutputPublication
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string Content { get; set; }
    public DateTime date { get; set; }

    public IEnumerable<DtoLike> likes;
    public IEnumerable<DtoPhotoPublication> photoPublications;
    public DtoVideoPublication videoPublications;

    public class DtoLike
    {
        public int Type { get; set; }
        public int Number { get; set; }
    }
    
    public class DtoPhotoPublication
    {
        public string Extension { get; set; }
        public string Photo { get; set; }
    }
    
    public class DtoVideoPublication
    {
        public string Extension { get; set; }
        public string Video { get; set; }
    }
}