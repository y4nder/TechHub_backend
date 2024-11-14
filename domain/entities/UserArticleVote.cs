namespace domain.entities;

public partial class UserArticleVote
{
    public int UserId { get; set; }

    public int ArticleId { get; set; }

    public short? VoteType { get; set; }

    public virtual Article Article { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
