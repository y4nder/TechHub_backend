using System.Security.Claims;
using domain.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace infrastructure.Hubs;

[Authorize]
public sealed class NotificationsHub : Hub<INotificationClient>
{
    private readonly IClubUserRepository _clubUserRepository;

    public NotificationsHub(IClubUserRepository clubUserRepository)
    {
        _clubUserRepository = clubUserRepository;
    }

    public override async Task OnConnectedAsync()
    {
        // Get the UserId from the JWT token claim
        var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId != null)
        {
            var clubs = await _clubUserRepository.GetClubNamesByUserId(int.Parse(userId));
            foreach (var clubName in clubs)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, clubName);
            }
        }
        
        await Clients.Client(Context.ConnectionId).ReceiveNotification($"Connecting user: {Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value}");
        await base.OnConnectedAsync(); 
    }
    
    public async Task JoinGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

    // Method to remove a client from a group
    public async Task LeaveGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }
    
    // Method to send notification to a specific user
    public async Task SendNotificationToUser(int targetUserId, string message)
    {
        // Optionally, check if the calling user is authorized to send notifications
        var senderUserId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (senderUserId == null)
        {
            // Handle unauthorized case
            return;
        }

        // Send the notification to the specific user based on their UserId
        await Clients.User(targetUserId.ToString()).ReceiveNotification(message);
    }
    
    public async Task SendNotificationToGroup(string groupName, string message)
    {
        await Clients.Group(groupName).ReceiveNotification(message);
    }
}

public interface INotificationClient 
{
    Task ReceiveNotification(string message);
}