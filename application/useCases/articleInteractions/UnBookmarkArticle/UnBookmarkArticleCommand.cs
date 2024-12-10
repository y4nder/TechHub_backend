using MediatR;

namespace application.useCases.articleInteractions.UnBookmarkArticle;

public class UnBookmarkArticleCommand : IRequest<UnBookmarkArticleResponse>
{
    public int ArticleId { get; set; }
    public int UserId { get; set; }
}

public class UnBookmarkArticleResponse
{
    public string Message { get; set; } = null!;
}