using domain.entities;
using domain.pagination;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.UserArticles;

public class GetUserArticlesQuery : IRequest<GetUserArticlesResponse>
{
    public int UserId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class GetUserArticlesResponse
{
    public string Message { get; set; } = String.Empty;
    public PaginatedResult<ArticleResponseDto> Articles { get; set; } = new();
}