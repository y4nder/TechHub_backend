using Microsoft.Extensions.Hosting;

namespace infrastructure.services.backgroundServices;

public class NotificationStorageService : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}