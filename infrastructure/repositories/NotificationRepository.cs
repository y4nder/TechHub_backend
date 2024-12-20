using domain.entities;
using domain.interfaces;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly AppDbContext _context;

    public NotificationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddNotification(Notification notification)
    {
        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync(); 
    }

    public Task<int> GetUnreadNotificationCountAsync(int userId)
    {
        throw new NotImplementedException();
        // return await _context.Notifications
        //     .CountAsync(n => n.UserId == userId && !n.IsRead);
    }

    public async Task<List<UserNotificationDtoResponse>> GetUserNotificationAsync(int userId)
    {
        return await _context.UserNotifications
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.Notification.CreatedAt)
            .Select(x => new UserNotificationDtoResponse
            {
                NotificationId = x.NotificationId,
                NotificationDto = x.Notification.Message,
                Read = x.IsRead
            }).ToListAsync();
    }

    // public Task<>
}