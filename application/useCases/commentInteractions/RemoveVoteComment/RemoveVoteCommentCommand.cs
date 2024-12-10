using MediatR;

namespace application.useCases.commentInteractions.RemoveVoteComment;

public class RemoveVoteCommentCommand : IRequest<RemoveVoteResponse>
{
    public int CommentId { get; set; }
    public int UserId { get; set; }
}

public class RemoveVoteResponse
{
    public string Message { get; set; } = null!;
}