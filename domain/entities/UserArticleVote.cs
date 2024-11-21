using domain.shared;

namespace domain.entities;

public partial class UserArticleVote
{
    public int UserId { get; set; }

    public int ArticleId { get; set; }

    public short VoteType { get; set; }

    public virtual Article Article { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public static UserArticleVote CreateUpvoteRecord(int userId, int articleId)
    {
        return new UserArticleVote
        {
            UserId = userId,
            ArticleId = articleId,
            VoteType = VoteTypes.Upvote
        };
    }
    
    public static UserArticleVote CreateDownVoteRecord(int userId, int articleId)
    {
        return new UserArticleVote
        {
            UserId = userId,
            ArticleId = articleId,
            VoteType = VoteTypes.DownVote
        };
    }
    
    
}

