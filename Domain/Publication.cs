namespace Domain;

public class Publication
{
    public int PublicationId { get; set; }
    public string PublicationContent { get; set; }
    public DateTime PublicationDate { get; set; }
    
    private int _commentCount;
    public int CommentCount { get => _commentCount == 0 ? _comments.Count : _commentCount; set { if (value > 0) _commentCount = value; } }

    private int _likeCount;
    public int LikeCount { get => _likeCount; set { if (_likeCount == 0 && value > 0) _likeCount = value; } }
    
    
    public IEnumerable<PublicationElement> Elements { get; set; }
    
    
    private readonly List<Comment> _comments = new();
    public IEnumerable<Comment> Comments() { return _comments; }
    public void Add(Comment comment) { if (_comments.All(f => f.CommentId != comment.CommentId)) { _comments.Add(comment); } }
    public void AddRange(IEnumerable<Comment> comments) { foreach (var comment in comments) Add(comment); }
}