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
    
    // todo add comment count

    public SingleQueryDto(
        int userId, 
        Article article, 
        ArticleBody articleBody,
        int voteCount)
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
    }
}