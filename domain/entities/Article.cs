namespace domain.entities;

public partial class Article
{
    public int ArticleId { get; set; }

    public int ArticleAuthorId { get; set; }

    public int ClubId { get; set; }

    public string ArticleTitle { get; set; } = null!;

    public string? ArticleThumbnailUrl { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public DateTime? UpdateDateTime { get; set; }

    public string Status { get; set; } = null!;

    public bool IsDrafted { get; set; }

    public bool Archived { get; set; }

    public virtual User? ArticleAuthor { get; set; }

    public virtual Club? Club { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<UserArticleBookmark> UserArticleBookmarks { get; set; } = new List<UserArticleBookmark>();

    public virtual ICollection<UserArticleRead> UserArticleReads { get; set; } = new List<UserArticleRead>();

    public virtual ICollection<UserArticleVote> UserArticleVotes { get; set; } = new List<UserArticleVote>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public static Article CreateDraft(ArticleDto dto)
    {
        return new Article
        {
            ArticleAuthorId = dto.AuthorId,
            ClubId = dto.ClubId,
            ArticleTitle = dto.ArticleTitle,
            ArticleThumbnailUrl = dto.ArticleThumbnailUrl,
            CreatedDateTime = DateTime.Now,
            UpdateDateTime = DateTime.Now,
            Status = ArticleStatusDefaults.Draft,
            IsDrafted = true,
            Tags = dto.Tags
        };
    }
    
    public static Article CreatePublished(ArticleDto dto)
    {
        return new Article
        {
            ArticleAuthorId = dto.AuthorId,
            ClubId = dto.ClubId,
            ArticleTitle = dto.ArticleTitle,
            ArticleThumbnailUrl = dto.ArticleThumbnailUrl,
            CreatedDateTime = DateTime.Now,
            UpdateDateTime = DateTime.Now,
            Status = ArticleStatusDefaults.Published,
            IsDrafted = false,
            Tags = dto.Tags
        };
    }
}

public class ArticleDto
{
    public int AuthorId { get; set; }
    public int ClubId { get; set; }
    public string ArticleTitle { get; set; } = null!;
    public string? ArticleThumbnailUrl { get; set; }
    public List<Tag> Tags { get; set; } = new List<Tag>();
}

public static class ArticleStatusDefaults
{
    public const string Draft = "Draft";
    public const string Published = "Published";
    public const string Archived = "Archived";
}

public class HomeArticle
{
    public int ArticleId { get; set; }
    public string ClubImageUrl { get; set; } = null!;
    public string UserImageUrl { get; set; } = null!;
    public string ArticleTitle { get; set; } = null!;
    public List<TagDto> Tags { get; set; } = null!;
    public DateTime? CreatedDateTime { get; set; }
    public string ArticleThumbnailUrl { get; set; } = null!;
    
    // TODO include article up votes and comment counts

    public static HomeArticle Create(Article article)
    {
        return new HomeArticle
        {
            ArticleId = article.ArticleId,
            ClubImageUrl = article.Club!.ClubImageUrl!,
            ArticleTitle = article.ArticleTitle,
            UserImageUrl = article.ArticleAuthor!.UserProfilePicUrl,
            Tags = article.Tags.Select(t => new TagDto
            {
                TagId = t.TagId,
                TagName = t.TagName
            }).ToList(),
            CreatedDateTime = article.CreatedDateTime,
            ArticleThumbnailUrl = article.ArticleThumbnailUrl!
        };
    }
}

