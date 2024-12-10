using MediatR;

namespace application.useCases.commentInteractions.ReplyAComment;

public class ReplyCommentCommand : IRequest<ReplyCommentResponse>
{
    public int ArticleId { get; set; }
    public int ParentCommentId { get; set; }
    public string Content { get; set; } = null!;
}

public class ReplyCommentResponse
{
    public string Message { get; set; } = null!;
}