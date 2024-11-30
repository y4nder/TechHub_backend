using domain.entities;
using domain.pagination;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.SearchedArticles;

public class SearchArticlesQuery : IRequest<SearchArticlesResponse>
{
    public string Query { get; set; } = null!;
    public int UserId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class SearchArticlesResponse
{
    public string Message { get; set; } = null!;

    public PaginatedResult<ArticleResponseDto> QueriedArticles { get; set; } = null!;
}