using application.Events.ArticleEvents;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.EventHandlers.ArticleEventHandlers;

public class UpdateTagCountEventHandler : INotificationHandler<ArticleCreatedEvent>
{
    private readonly ITagRepository _tagRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTagCountEventHandler(ITagRepository tagRepository, IUnitOfWork unitOfWork)
    {
        _tagRepository = tagRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ArticleCreatedEvent notification, CancellationToken cancellationToken)
    {
        var tags = notification.Article.Tags;

        foreach (var tag in tags)
        {
            tag.TagCount += 1;
        }
        
        _tagRepository.BatchTagUpdate(tags);
        
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}