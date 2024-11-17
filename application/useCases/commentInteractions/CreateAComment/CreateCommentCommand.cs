using MediatR;

namespace application.useCases.commentInteractions.CreateAComment;

public class CreateCommentCommand : IRequest<CreateCommentResponse>
{
    public int CommentCreatorId { get; set; }
    public int ArticleId { get; set; }
    public string Content { get; set; } = null!;
}

public class CreateCommentResponse
{
    public string Message { get; set; } = null!;
}