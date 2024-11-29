using domain.entities;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.SingleArticle;

public class SingleArticleQuery : IRequest<SingleQueryDto>
{
    public int UserId { get; set; }
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
    public string ArticleContent { get; set; }
    public int VoteCount { get; set; }
    public int CommentCount { get; set; }
    public int VoteType { get; set; }
    public bool Bookmarked { get; set; }
    
    // public static SingleQueryDto Create(int requestId, Article article, ArticleBody articleBody, int voteCount, int commentCount)
    // {
    //     return new SingleQueryDto(requestId, article, articleBody, voteCount);
    // }
    
    public static SingleQueryDto Create(int requestId, Article article, ArticleBody articleBody, 
        int voteCount, int commentCount, int voteType, bool bookmarked)
    {
        return new SingleQueryDto(requestId, article, articleBody, voteCount, commentCount, voteType, bookmarked);
    }
    
    public SingleQueryDto(
        int userId, 
        Article article, 
        ArticleBody articleBody,
        int voteCount,
        int commentCount,
        int voteType,
        bool bookmarked
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
        ArticleContent = articleBody.ArticleContent;
        VoteCount = voteCount;
        CommentCount = commentCount;
        VoteType = voteType;
        Bookmarked = bookmarked;
    }
}

public class VoteTypeConstants
{
    public const int DownVoted = -1;
    public const int NotVoted = 0;
    public const int UpVoted = 1;
}