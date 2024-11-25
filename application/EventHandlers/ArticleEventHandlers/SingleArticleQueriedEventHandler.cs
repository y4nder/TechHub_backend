using application.Events.ArticleEvents;
using domain.entities;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.EventHandlers.ArticleEventHandlers;

public class SingleArticleQueriedEventHandler : INotificationHandler<SingleArticleQueriedEvent>
{
    private readonly IUserArticleReadRepository _userArticleReadRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SingleArticleQueriedEventHandler(
        IUserArticleReadRepository userArticleReadRepository,
        IUnitOfWork unitOfWork)
    {
        _userArticleReadRepository = userArticleReadRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(SingleArticleQueriedEvent notification, CancellationToken cancellationToken)
    {
        var userArticleRead = await _userArticleReadRepository.GetUserArticleRead(notification.UserId, notification.ArticleId);

        if (userArticleRead == null)
        {
            var userArticleReadRecord = UserArticleRead.Create(notification.UserId, notification.ArticleId);
            _userArticleReadRepository.AddReadHistory(userArticleReadRecord);
        }
        else
        {
            userArticleRead.UpdateReadDateTimeToNow();
        }
        
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}