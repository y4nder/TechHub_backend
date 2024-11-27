using MediatR;

namespace application.useCases.articleInteractions.RemoveArticleVote;

public class RemoveArticleVoteCommand : IRequest<RemovedArticleVoteResponse>
{
    public int UserId { get; set; }
    public int ArticleId { get; set; } 
}

public class RemovedArticleVoteResponse
{
    public string Message { get; set; } = null!;
}