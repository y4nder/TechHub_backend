using MediatR;

namespace application.useCases.articleInteractions.UpvoteArticle;

public class UpVoteArticleCommand : IRequest<UpVoteArticleResponse>
{
    public int UserId { get; set; }
    public int ArticleId { get; set; }
}

public class UpVoteArticleResponse
{
    public string Message { get; set; } = null!;
}