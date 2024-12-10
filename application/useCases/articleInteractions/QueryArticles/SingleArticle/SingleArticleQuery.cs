using domain.entities;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.SingleArticle;

public class SingleArticleQuery : IRequest<SingleQueryDto>
{
    public int ArticleId { get; set; }
}

public class SingleQueryDto
{
    public int UserId { get; set; }
    public int ArticleId { get; set; }  
    public string ArticleTitle { get; set; }
    public List<TagDto> Tags { get; set; } 
    public DateTime? CreatedDateTime { get; set; }
    public DateTime? UpdatedDateTime { get; set; }
    public UserMinimalDto Author { get; set; } 
    
    public string ArticleHtmlContent { get; set; }
    public int VoteCount { get; set; }
    public int CommentCount { get; set; }
    public int VoteType { get; set; }
    public bool Bookmarked { get; set; }
    public bool Followed { get; set; }
    public bool IsOwned { get; set; }


    public static SingleQueryDto Create(int requestId, Article article, string articleHtmlContent, 
        int voteCount, int commentCount, int voteType, bool bookmarked, bool followed, bool isOwned)
    {
        return new SingleQueryDto(requestId, article, articleHtmlContent, voteCount, commentCount, voteType, bookmarked, followed, isOwned);
    }
    
    public SingleQueryDto(
        int userId, 
        Article article, 
        string articleHtmlContent,
        int voteCount,
        int commentCount,
        int voteType,
        bool bookmarked,
        bool followed,
        bool isOwned
    )
    {
        UserId = userId;
        ArticleId = article.ArticleId;
        ArticleTitle = article.ArticleTitle;
        Tags = article.Tags.Select(t 
            => new TagDto
            {
                TagId = t.TagId, TagName = t.TagName
            }).ToList();
        CreatedDateTime = article.CreatedDateTime;
        UpdatedDateTime = article.UpdateDateTime;
        Author = new UserMinimalDto(article.ArticleAuthor!);
        ArticleHtmlContent = articleHtmlContent;
        VoteCount = voteCount;
        CommentCount = commentCount;
        VoteType = voteType;
        Bookmarked = bookmarked;
        Followed = followed;
        IsOwned = isOwned;
    }
}

public class VoteTypeConstants
{
    public const int DownVoted = -1;
    public const int NotVoted = 0;
    public const int UpVoted = 1;
}