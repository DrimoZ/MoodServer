namespace Domain;

public class Publication
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }
    
    
    private int _commentCount;
    public int CommentCount { get => _commentCount == 0 ? _comments.Count : _commentCount; set { if (_commentCount == 0 && value > 0) _commentCount = value; } }

    private int _likeCount;
    public int LikeCount { get => _likeCount; set { if (_likeCount == 0 && value > 0) _likeCount = value; } }
    
    
    public IEnumerable<PublicationElement> Elements { get; set; }
    
    
    private readonly List<Comment> _comments = new();
    public IEnumerable<Comment> Comments() { return _comments; }
    public void Add(Comment comment) { if (_comments.All(f => f.Id != comment.Id)) { _comments.Add(comment); } }
    public void AddRange(IEnumerable<Comment> comments) { foreach (var comment in comments) Add(comment); }
}