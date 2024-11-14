namespace domain.entities;

public partial class Article
{
    public int ArticleId { get; set; }

    public int? ArticleAuthorId { get; set; }

    public int? ClubId { get; set; }

    public string? ArticleThumbnailUrl { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public DateTime? UpdateDateTime { get; set; }

    public string? Status { get; set; }

    public bool? IsDrafted { get; set; }

    public virtual User? ArticleAuthor { get; set; }

    public virtual Club? Club { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<UserArticleBookmark> UserArticleBookmarks { get; set; } = new List<UserArticleBookmark>();

    public virtual ICollection<UserArticleRead> UserArticleReads { get; set; } = new List<UserArticleRead>();

    public virtual ICollection<UserArticleVote> UserArticleVotes { get; set; } = new List<UserArticleVote>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
