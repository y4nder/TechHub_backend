using MediatR;

namespace application.useCases.commentInteractions.DownVoteComment;

public class DownVoteCommentCommand : IRequest<DownVoteCommentResponse>
{
    public int CommentId { get; set; }
    public int UserId { get; set; }
}

public class DownVoteCommentResponse
{
    public string Message { get; set; } = null!;
}