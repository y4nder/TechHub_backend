using MediatR;

namespace application.Events.ArticleEvents;

public class SingleArticleQueriedEvent : INotification
{
    public int UserId { get; private set; }
    public int ArticleId { get; private set; }

    public SingleArticleQueriedEvent(int userId, int articleId)
    {
        UserId = userId;
        ArticleId = articleId;
    }
}