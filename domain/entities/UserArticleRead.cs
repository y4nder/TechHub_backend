namespace domain.entities;

public partial class UserArticleRead
{
    public int UserId { get; set; }

    public int ArticleId { get; set; }

    public DateTime? ReadDateTime { get; set; }

    public virtual Article Article { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
