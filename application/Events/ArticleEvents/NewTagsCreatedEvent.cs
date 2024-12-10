using domain.entities;
using MediatR;

namespace application.Events.ArticleEvents;

public class NewTagsCreatedEvent : INotification
{
    public Article Article { get; set; } = null!;

    public NewTagNotification NewTagNotification { get; set; } = null!;

}