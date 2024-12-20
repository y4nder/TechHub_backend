using infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace infrastructure.services.Notification;

public class NotificationService 
{
    private readonly IHubContext<NotificationsHub, INotificationClient> _hubContext;

    public NotificationService(IHubContext<NotificationsHub, INotificationClient> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyUser(int targetUserId, string message)
    {
        await _hubContext.Clients.User(targetUserId.ToString()).ReceiveNotification(message);
    }
}