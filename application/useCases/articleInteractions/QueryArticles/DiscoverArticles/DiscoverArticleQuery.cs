using domain.entities;
using domain.pagination;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.DiscoverArticles;

public class DiscoverArticleQuery : IRequest<DiscoverArticleResponse>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class DiscoverArticleResponse
{
    public string Message { get; set; } = null!;
    public PaginatedResult<ArticleResponseDto> Articles { get; set; } = null!;
}