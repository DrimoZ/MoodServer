namespace Domain;
public class User
{
    public string Id { get; set; }
    
    public UserRole Role { get; set; }
    public string Login { get; set; }
    public string Mail { get; set; }
    
    public string Name { get; set; }
    public string Title { get; set; }
    
    public Account Account { get; set; }

    private readonly List<User> _friends = new();
    private readonly List<Publication> _publications = new();



    public int TotalFriends()
    {
        return _friends.Count;
    }
    
    public int TotalPublications()
    {
        return _publications.Count;
    }
    
    public IEnumerable<User> Friends()
    {
        return _friends;
    }
    
    public IEnumerable<Publication> Publications()
    {
        return _publications;
    }
    
    public void Add(User friend)
    {
        if (_friends.All(f => f.Id != friend.Id))
        {
            _friends.Add(friend);
        }
    }
    
    public void Add(Publication publication)
    {
        if (_publications.All(p => p.Id != publication.Id))
        {
            _publications.Add(publication);
        }
    }

    public void AddRange(IEnumerable<User> users)
    {
        foreach (var friend in users)
            Add(friend);
    }
    
    public void AddRange(IEnumerable<Publication> publications)
    {
        foreach (var publication in publications)
            Add(publication);
    }
    
    public override string ToString()
    {
        var friendsString = string.Join("\n\t\t", _friends.Select(f => f.ToString()));
        return $"User: \n\tId={Id}, \n\tRole={Role}, \n\tLogin={Login}, \n\tMail={Mail}, \n\tName={Name}, \n\tTitle={Title}, \n\tTotalFriends={TotalFriends()}, \n\tTotalPublications={TotalPublications()}, \n\tAccount={Account?.ToString() ?? "No Account"}, \n\tFriends=[{friendsString}]";
    }
}
