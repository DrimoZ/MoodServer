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
    
    public List<User> Friends { get; set; }
    public List<Publication> Publications { get; set; }
}
