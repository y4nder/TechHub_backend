using MediatR;

namespace application.useCases.commentInteractions.UpvoteComment;

public class UpvoteCommentCommand : IRequest<UpvoteCommandResponse>
{
    public int CommentId { get; set; }
    public int UserId { get; set; }
}

public class UpvoteCommandResponse
{
    public string Message { get; set; } = null!;
}