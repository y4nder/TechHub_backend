using infrastructure.Hubs;
using Microsoft.AspNetCore.Builder;

namespace infrastructure.configurations;

public static class WebApplicationInfrastructureExtensions
{
    public static void AddNotificationHub(this WebApplication app)
    {
        app.MapHub<NotificationsHub>("/notifications");
    }
}