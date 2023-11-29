namespace Application.Dtos.User;

public class DtoInputCreateUser
{
    public string Id { get; set; }
    
    public string Mail { get; set; }
    public string Login { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    
    public DtoAccount Account { get; set; }
    
    public class DtoAccount
    {
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public DateTime BirthDate { get; set; }
    }
}