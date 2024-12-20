using application.Events.ArticleEvents;
using application.useCases.ModeratorInteractions.PinActions;
using application.utilities.NotificationParser;
using domain.entities;
using domain.interfaces;
using infrastructure.Hubs;
using infrastructure.services.worker;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace application.EventHandlers.ArticleEventHandlers;

public class CreateArticlePostedNotificationHandler : INotificationHandler<ArticleCreatedEvent> 
{
    private readonly IUserRepository _userRepository;
    private readonly NotificationUtility _notificationUtility;
    private readonly IClubUserRepository _clubUserRepository;
    private readonly IClubRepository _clubRepository;
    private readonly INotificationRepository _notificationRepository;
    private readonly IUserNotificationRepository _userNotificationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<NotificationsHub, INotificationClient> _hubContext; 


    public CreateArticlePostedNotificationHandler(IUserRepository userRepository, NotificationUtility notificationUtility, IClubUserRepository clubUserRepository, INotificationRepository notificationRepository, IUnitOfWork unitOfWork, IUserNotificationRepository userNotificationRepository, IClubRepository clubRepository, IHubContext<NotificationsHub, INotificationClient> hubContext)
    {
        _userRepository = userRepository;
        _notificationUtility = notificationUtility;
        _clubUserRepository = clubUserRepository;
        _notificationRepository = notificationRepository;
        _unitOfWork = unitOfWork;
        _userNotificationRepository = userNotificationRepository;
        _clubRepository = clubRepository;
        _hubContext = hubContext;
    }

    public async Task Handle(ArticleCreatedEvent notification, CancellationToken cancellationToken)
    {
        // create unique id
        var notificationId = Guid.NewGuid();
        
        var postedArticle = notification.Article;
        var postedArticleArticleAuthorId = postedArticle.ArticleAuthorId;
        
        var user = await _userRepository.GetUserById(postedArticleArticleAuthorId)??
                   throw new KeyNotFoundException($"User with id {postedArticleArticleAuthorId} not found");
        
        //send notification
        var club = await _clubRepository.GetClubByIdNo(postedArticle.ClubId)??
                   throw new KeyNotFoundException($"Club with id {postedArticle.ClubId} not found");
        
        //create dto
        var notificationDto = new NotificationDto
        {
            NotificationId = notificationId,
            NotificationSubject = new NotificationSubject
            {
                SubjectName = user.Username!,
                SubjectProfilePic = user.UserProfilePicUrl,
            },
            Action = NotificationActionConstants.ArticleShared,
            NotificationObject = new NotificationObject
            {
                ObjectId = club.ClubId,
                Title = club.ClubName!
            },
            SubDetails = postedArticle.ArticleTitle,
            PreviewId = postedArticle.ArticleId,
            PreviewImageUrl = postedArticle.ArticleThumbnailUrl!
        };
        
        // parse notification
        string parsedNotification = _notificationUtility.Parse(notificationDto);
        
        //save notification
        var notificationRecord = new Notification(parsedNotification);
        await _notificationRepository.AddNotification(notificationRecord);
        await _unitOfWork.CommitAsync(cancellationToken);

        List<int> memberIds = await _clubUserRepository.GetMemberIds(postedArticle.ClubId);
        var userInfos = memberIds.Select(id => new UserNotification
        {
            NotificationId = notificationRecord.NotificationId,
            UserId = id,
            ArticleId = postedArticle.ArticleId,
        });
        
        _userNotificationRepository.AddNotificationRange(userInfos);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        await _hubContext.Clients.Group(club!.ClubName!).ReceiveNotification(parsedNotification);
        
    }
}