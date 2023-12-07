namespace Application.Dtos.User.UserData;

public class DtoInputUpdateUser
{
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Title { get; set; }
        public string Mail { get; set; }
        //Account
        public DateTime Birthdate { get; set; }
        public string Description { get; set; }
}