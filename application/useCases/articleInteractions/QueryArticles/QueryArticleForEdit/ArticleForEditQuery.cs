using domain.entities;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.QueryArticleForEdit;

public class ArticleForEditQuery : IRequest<ArticleForEditResponse>
{
    public int ArticleId { get; set; }
}

public class ArticleForEditResponse
{
    public string Message { get; set; } = null!;
    public ArticleResponseForEditDto Article { get; set; } = null!;
}