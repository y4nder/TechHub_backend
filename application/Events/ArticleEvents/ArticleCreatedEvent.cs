using domain.entities;
using MediatR;

namespace application.Events.ArticleEvents;

public class ArticleCreatedEvent : INotification
{
    public Article Article { get; set; } = null!;
    public string ArticleContent { get; set; } = null!;

    public NewTagNotification NewTagNotification { get; set; } = null!;
}

public class NewTagNotification
{
    public bool HasNewTags { get; private set; }
    public List<string>? NewTags { get; private set; }

    public static NewTagNotification CreateHasNewTags(List<string> newTags)
    {
        return new NewTagNotification { HasNewTags = true, NewTags = newTags };
    }

    public static NewTagNotification CreateHasNoNewTags()
    {
        return new NewTagNotification { HasNewTags = false, NewTags = null };
    }
} 