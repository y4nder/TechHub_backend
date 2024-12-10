using application.utilities.UserContext;
using domain.entities;
using domain.interfaces;
using domain.pagination;
using MediatR;

namespace application.useCases.commentInteractions.QueryComments.QueryCommentsForArticleNew;

public class GetSingleArticleCommentsQuery: IRequest<SingleArticleCommentsResponse>
{
    public int ArticleId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class SingleArticleCommentsResponse
{
    public string Message { get; set; } = null!;
    public PaginatedResult<CommentItemDto> Comments { get; set; } = new();
}

public class GetSingleArticleCommentsQueryHandler : IRequestHandler<GetSingleArticleCommentsQuery, SingleArticleCommentsResponse>
{
    private readonly IUserContext _userContext;
    private readonly IArticleRepository _articleRepository;
    private readonly ICommentRepository _commentRepository;

    public GetSingleArticleCommentsQueryHandler(
        IUserContext userContext, 
        IArticleRepository articleRepository, ICommentRepository commentRepository)
    {
        _userContext = userContext;
        _articleRepository = articleRepository;
        _commentRepository = commentRepository;
    }

    public async Task<SingleArticleCommentsResponse> Handle(GetSingleArticleCommentsQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();
        
        if (! await _articleRepository.ArticleExistsByIdIgnoreArchived(request.ArticleId))
            throw new KeyNotFoundException("Article does not exist");
        
        if(request.PageNumber <= 0 || request.PageSize <= 0 || request.PageNumber >= request.PageSize)
            throw new InvalidOperationException("Invalid page number or page size");
        
        var comments = await _commentRepository
            .GetParentComments(userId, request.ArticleId, request.PageNumber, request.PageSize);

        return new SingleArticleCommentsResponse
        {
            Message = "Comments retrieved",
            Comments = comments
        };
    }
}