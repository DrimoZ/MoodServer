namespace Application.Services.Users;

public interface IFriendService
{
    int GetFriendStatus(string connectedUserId, string userToFetch);
}