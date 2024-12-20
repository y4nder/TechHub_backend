using domain.entities;
using domain.interfaces;
using infrastructure.UserContext;
using MediatR;

namespace application.useCases.notificationInteractions.notificationQueries;

public record AllNotificationQuery() : IRequest<List<UserNotificationDtoResponse>>;

public class AllNotificationQueryHandler : IRequestHandler<AllNotificationQuery, List<UserNotificationDtoResponse>>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUserContext _userContext;

    public AllNotificationQueryHandler(INotificationRepository notificationRepository, IUserContext userContext)
    {
        _notificationRepository = notificationRepository;
        _userContext = userContext;
    }

    public async Task<List<UserNotificationDtoResponse>> Handle(AllNotificationQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();
        
        return await _notificationRepository.GetUserNotificationAsync(userId);
    }
}