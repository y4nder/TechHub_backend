using application.Events.ArticleEvents;
using domain.entities;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.EventHandlers.ArticleEventHandlers;

public class CreateArticleBodyEventHandler : INotificationHandler<ArticleCreatedEvent>
{
    private readonly IArticleBodyRepository _articleBodyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateArticleBodyEventHandler(IArticleBodyRepository articleBodyRepository, IUnitOfWork unitOfWork)
    {
        _articleBodyRepository = articleBodyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ArticleCreatedEvent notification, CancellationToken cancellationToken)
    {
        var articleBody = ArticleBody.Create(notification.Article.ArticleId, notification.ArticleContent);
        
        _articleBodyRepository.AddArticleBody(articleBody);
        
        await _unitOfWork.CommitAsync(cancellationToken); 
    }
}