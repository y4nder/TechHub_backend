namespace domain.entities;

public partial class UserArticleBookmark
{
    public int UserId { get; set; }

    public int ArticleId { get; set; }

    public DateTime? BookmarkDateTime { get; set; }

    public virtual Article Article { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
