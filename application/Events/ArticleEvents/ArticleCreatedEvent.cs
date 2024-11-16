using domain.entities;
using MediatR;

namespace application.Events.ArticleEvents;

public class ArticleCreatedEvent : INotification
{
    public Article Article { get; init; }
    public string ArticleContent { get; init; }
    
    public ArticleCreatedEvent(Article article , string articleContent)
    {
        Article = article;
        ArticleContent = articleContent;
    }
}