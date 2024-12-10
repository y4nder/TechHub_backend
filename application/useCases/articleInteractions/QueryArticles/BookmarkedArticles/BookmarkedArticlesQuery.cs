using domain.entities;
using domain.pagination;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.BookmarkedArticles;

public class BookmarkedArticlesQuery : IRequest<BookmarkedArticlesResponse>
{
    public int UserId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class BookmarkedArticlesResponse
{
    public string Message { get; set; } = null!;
    public PaginatedResult<ArticleResponseDto> Articles { get; set; } = new();
}