using domain.entities;
using domain.pagination;
using MediatR;

namespace application.useCases.commentInteractions.QueryComments.QueryUserReplies;

public class UserRepliesQuery : IRequest<UserRepliesResponse>
{
    public int UserId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class UserRepliesResponse
{
    public required string Message { get; set; }
    public PaginatedResult<UserReplyDto> Replies { get; set; } = new();
}