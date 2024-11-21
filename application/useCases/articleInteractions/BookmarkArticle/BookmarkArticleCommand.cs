using MediatR;

namespace application.useCases.articleInteractions.BookmarkArticle;

public class BookmarkArticleCommand : IRequest<BookmarkArticlResponse>
{
    public int ArticleId { get; set; }
    public int UserId { get; set; }
}

public class BookmarkArticlResponse
{
    public string Message { get; set; } = null!;
}