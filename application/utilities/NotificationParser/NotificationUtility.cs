using System.Text.Json;
using domain.entities;

namespace application.utilities.NotificationParser;

public class NotificationUtility
{
    public string Parse(NotificationDto notificationDto) => JsonSerializer.Serialize(notificationDto);
}