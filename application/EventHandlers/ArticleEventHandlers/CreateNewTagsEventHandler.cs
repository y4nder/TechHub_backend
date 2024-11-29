using application.Events.ArticleEvents;
using domain.entities;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.EventHandlers.ArticleEventHandlers;

public class CreateNewTagsEventHandler : INotificationHandler<ArticleCreatedEvent>
{
    private readonly ITagRepository _tagRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateNewTagsEventHandler(ITagRepository tagRepository, IUnitOfWork unitOfWork)
    {
        _tagRepository = tagRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ArticleCreatedEvent notification, CancellationToken cancellationToken)
    {
        var newTagNotification = notification.NewTagNotification;
        var article = notification.Article;

        if (newTagNotification.HasNewTags == false)
            return;
        
        var newTags = newTagNotification.NewTags;

        var createdTags = newTags!.Select(newTagName => new Tag
        {
            TagName = newTagName,
            TagCount = 1
        }).ToList();

        foreach (var createdTag in createdTags)
        {
            article.Tags.Add(createdTag);
        }
        
        _tagRepository.BatchAddTags(createdTags);
        
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}