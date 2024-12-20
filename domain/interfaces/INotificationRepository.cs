using domain.entities;

namespace domain.interfaces;

public interface INotificationRepository
{
    Task AddNotification(Notification notification);

    Task<int> GetUnreadNotificationCountAsync(int userId);
    
    Task<List<UserNotificationDtoResponse>> GetUserNotificationAsync(int userId);
}