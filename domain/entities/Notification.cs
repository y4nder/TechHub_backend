namespace domain.entities;

public class Notification
{
    public Guid NotificationId { get; private set; }
     
    public string Message { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    
  

    private Notification() { }

    public Notification(string message)
    {
        NotificationId = Guid.NewGuid();
        Message = message;
        CreatedAt = DateTime.UtcNow;
    }
    
}

public class UserNotificationDtoResponse
{
    public Guid NotificationId { get; set; }
    public string NotificationDto { get; set; } = null!;
    public bool Read { get; set; }
}

public class UserNotification
{
    public int UserId { get; set; }
    public User User { get; set; }  = null!;

    public Guid NotificationId { get; set; }
    public Notification Notification { get; set; } = null!; 
    public bool IsRead { get; set; } = false;
    public DateTime ReadAt { get; set; } = DateTime.UtcNow;
    public int? ArticleId { get; set; }
    public Article? Article { get; set; }
    
    public int? CommentId { get; set; }
    public Comment? Comment { get; set; }
}

public class NotificationDto
{
    public Guid NotificationId { get; set; }
    public NotificationSubject NotificationSubject { get; set; } = new();
    public string Action { get; set; } = string.Empty;
    public NotificationObject NotificationObject { get; set; } = new();
    
    public string SubDetails { get; set; } = string.Empty;
    public int PreviewId { get; set; }
    public string PreviewImageUrl { get; set; } = string.Empty;
}



public class NotificationSubject
{
    public string SubjectName { get; set; } = null!;
    public string SubjectProfilePic { get; set; } = null!;
}

public class NotificationObject
{
    public int ObjectId { get; set; }
    public string Title { get; set; } = string.Empty;
}

public class NotificationActionConstants
{
    public const string ArticleShared = " shared a new article on ";
}