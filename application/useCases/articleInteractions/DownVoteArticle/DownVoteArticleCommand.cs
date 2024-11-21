using application.useCases.articleInteractions.UpvoteArticle;
using MediatR;

namespace application.useCases.articleInteractions.DownVoteArticle;

public class DownVoteArticleCommand : IRequest<DownVoteArticleCommandResponse>
{
    public int UserId { get; set; }
    public int ArticleId { get; set; }
}

public class DownVoteArticleCommandResponse
{
    public string Message { get; set; } = null!;
}