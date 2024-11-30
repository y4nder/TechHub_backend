using domain.entities;
using domain.pagination;
using MediatR;

namespace application.useCases.commentInteractions.QueryComments.QueryCommentsForSingleArticle;

public class GetArticleCommentsQuery : IRequest<ArticleCommentsResponse> 
{
    public int UserId { get; set; }
    public int ArticleId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class ArticleCommentsResponse
{
    public string Message { get; set; } = null!;
    public PaginatedResult<ArticleCommentDto> Comments { get; set; } = null!;
}