using infrastructure.Hubs;
using infrastructure.UserContext;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace infrastructure.services.backgroundServices;

public class ServerTimeNotifier : BackgroundService
{
    private static readonly TimeSpan ServerTimeNotifierInterval = TimeSpan.FromSeconds(7);
    private readonly ILogger<ServerTimeNotifier> _logger;
    private readonly IHubContext<NotificationsHub, INotificationClient> _hubContext;
    
    
    public ServerTimeNotifier(ILogger<ServerTimeNotifier> logger, IHubContext<NotificationsHub, INotificationClient> hubContext)
    {
        _logger = logger;
        _hubContext = hubContext;
        
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        using var timer = new PeriodicTimer(ServerTimeNotifierInterval);
        
        

        while (!stoppingToken.IsCancellationRequested &&
               await timer.WaitForNextTickAsync(stoppingToken))
        {
            var dateTime = DateTime.Now;
            
            _logger.LogInformation("Executing {Service} {Time}", nameof(ServerTimeNotifier), dateTime);

            await _hubContext.Clients.User(userId:"7").ReceiveNotification($"Server time = {dateTime} " );
        }
    }
}