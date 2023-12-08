
namespace Application.Dtos.User.UserData;

public class DtoOutputProfileUser
{
    public string Id { get; set; }
    
    public string Login { get; set; }
    public string Name { get; set; }

    public string Mail { get; set; }
    public string Title { get; set; }
    
    public int PublicationCount { get; set; }
    public int FriendCount { get; set; }
    
    public DtoOutputAccount Account { get; set; }
    
    public IEnumerable<DtoOutputProfileUser> Friends { get; set; }
    public IEnumerable<DtoOutputPublication> Publications { get; set; }

    
    public class DtoOutputAccount
    {
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public DateTime BirthDate { get; set; }
    }
    
    public class DtoOutputPublication
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime date { get; set; }
    }
}