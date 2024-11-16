using MediatR;

namespace application.useCases.articleInteractions.ArchiveArticle;

public class ArchiveArticleCommand : IRequest<ArchiveArticleResponse>
{
    public int ArticleId { get; set; }
    public int AuthorId { get; set; }
}

public class ArchiveArticleResponse
{
    public string Message { get; set; } = null!;
}