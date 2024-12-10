using domain.entities;
using domain.pagination;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.HomeArticles;

public class GetHomeArticlesQuery : IRequest<HomeArticleResponse>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class HomeArticleResponse
{
    public string Message { get; set; } = null!;
    public PaginatedResult<ArticleResponseDto> Articles { get; set; } = null!;
}

