using Application.Dtos.Group;
using Application.Dtos.Message;
using Application.UseCases.Groups;
using Application.UseCases.Messages;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Hubs;

public class ChatHub : Hub
{

    public async Task SendMessageToGroup(DtoInputMessage message, DtoOutputGroup group)
    {
        await Clients.Group(group.Id.ToString()).SendAsync("ReceiveMessage", group);
    }

    public async Task RemoveMessageFromGroup(DtoOutputGroup group)
    {
        await Clients.Group(group.Id.ToString()).SendAsync("ReceiveMessage", group);
    }
    
    public async Task AddToGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        // await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
    }
    
    public async Task RemoveFromGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        // await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
    }
}