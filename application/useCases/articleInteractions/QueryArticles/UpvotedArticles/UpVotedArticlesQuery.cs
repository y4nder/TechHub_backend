using domain.entities;
using domain.pagination;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.UpvotedArticles;

public class UpVotedArticlesQuery : IRequest<UpVotedArticlesResponse>
{
    public int UserId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class UpVotedArticlesResponse
{
    public string Message { get; set; } = String.Empty;
    public PaginatedResult<ArticleResponseDto> Articles { get; set; } = new();
}