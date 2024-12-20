using domain.entities;

namespace domain.interfaces;

public interface IUserNotificationRepository
{
    void AddNotificationRange(IEnumerable<UserNotification> notifications);
}