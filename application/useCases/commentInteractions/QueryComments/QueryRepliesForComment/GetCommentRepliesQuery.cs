using domain.entities;
using domain.interfaces;
using domain.pagination;
using infrastructure.UserContext;
using MediatR;

namespace application.useCases.commentInteractions.QueryComments.QueryRepliesForComment;

public class GetCommentRepliesQuery : IRequest<GetCommentRepliesQueryResponse>
{
    public int ParentCommentId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class GetCommentRepliesQueryResponse
{
    public string Message { get; set; } = null!;
    public PaginatedResult<CommentItemDto> Comments { get; set; } = new();
}

public class GetCommentRepliesQueryHandler : IRequestHandler<GetCommentRepliesQuery, GetCommentRepliesQueryResponse>
{
    private readonly IUserContext _userContext;
    private readonly ICommentRepository _commentRepository;

    public GetCommentRepliesQueryHandler(IUserContext userContext, ICommentRepository commentRepository)
    {
        _userContext = userContext;
        _commentRepository = commentRepository;
    }

    public async Task<GetCommentRepliesQueryResponse> Handle(GetCommentRepliesQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();
        
        if(request.PageNumber <= 0 || request.PageSize <= 0 || request.PageNumber >= request.PageSize)
            throw new InvalidOperationException("Invalid page number or page size");
        
        var replies = await _commentRepository
            .GetArticleCommentReplies(userId,  request.ParentCommentId, request.PageNumber, request.PageSize);

        return new GetCommentRepliesQueryResponse
        {
            Comments = replies,
            Message = "Replies Retrieved"
        };
    }
}

