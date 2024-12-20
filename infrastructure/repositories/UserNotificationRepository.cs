using domain.entities;
using domain.interfaces;

namespace infrastructure.repositories;

public class UserNotificationRepository : IUserNotificationRepository
{
    private readonly AppDbContext _context;

    public UserNotificationRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddNotificationRange(IEnumerable<UserNotification> notifications)
    {
        _context.UserNotifications.AddRange(notifications);
    }
}