namespace domain.entities;

public partial class UserArticleBookmark
{
    public int UserId { get; set; }

    public int ArticleId { get; set; }

    public DateTime? BookmarkDateTime { get; set; }

    public virtual Article Article { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public static UserArticleBookmark Create(int userId, int articleId)
    {
        return new UserArticleBookmark
        {
            UserId = userId,
            ArticleId = articleId,
            BookmarkDateTime = DateTime.Now
        };
    }
}
