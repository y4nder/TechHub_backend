using application.Events.ArticleEvents;
using domain.entities;
using domain.interfaces;
using infrastructure.services.httpImgInterceptor;
using infrastructure.services.worker;
using MediatR;

namespace application.EventHandlers.ArticleEventHandlers;

public class CreateArticleBodyEventHandler : INotificationHandler<ArticleCreatedEvent>
{
    private readonly IArticleBodyRepository _articleBodyRepository;
    private readonly IHtmlImageProcessor _htmlImageProcessor;
    private readonly IContentImageProcessor _contentImageProcessor;
    private readonly IUnitOfWork _unitOfWork;

    public CreateArticleBodyEventHandler(
        IArticleBodyRepository articleBodyRepository,
        IUnitOfWork unitOfWork,
        IHtmlImageProcessor htmlImageProcessor,
        IContentImageProcessor contentImageProcessor)
    {
        _articleBodyRepository = articleBodyRepository;
        _unitOfWork = unitOfWork;
        _htmlImageProcessor = htmlImageProcessor;
        _contentImageProcessor = contentImageProcessor;
    }

    public async Task Handle(ArticleCreatedEvent notification, CancellationToken cancellationToken)
    {
        string processedHtml = "";
        string processedContent = "";
        
        try
        {
            processedHtml = await _htmlImageProcessor.ProcessHtmlContentAsync(notification.ArticleHtmlContent);
            processedContent = await _contentImageProcessor.ProcessContentAsync(notification.ArticleContent);
        }
        catch (Exception ex)
        {
            processedHtml = notification.ArticleHtmlContent;
            processedContent = notification.ArticleContent;
        }

        var articleBody = ArticleBody.Create(notification.Article.ArticleId, notification.ArticleContent, processedHtml);
        _articleBodyRepository.AddArticleBody(articleBody);
        await _unitOfWork.CommitAsync(cancellationToken); 
    }
}