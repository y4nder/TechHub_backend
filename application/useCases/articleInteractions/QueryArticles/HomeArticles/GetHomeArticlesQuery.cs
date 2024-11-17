using domain.entities;
using domain.pagination;
using MediatR;

namespace application.useCases.articleInteractions.QueryArticles.HomeArticles;

public class GetHomeArticlesQuery : IRequest<HomeArticleResponse>
{
    public int UserId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class HomeArticleResponse
{
    public string Message { get; set; } = null!;
    public PaginatedResult<HomeArticle> HomeArticles { get; set; } = null!;
}

public class HomeArticle
{
    public int ArticleId { get; set; }
    public string ClubImageUrl { get; set; } = null!;
    public string UserImageUrl { get; set; } = null!;
    public string ArticleTitle { get; set; } = null!;
    public List<HomeArticleTag> Tags { get; set; } = null!;
    public DateTime? CreatedDateTime { get; set; }
    public string ArticleThumbnailUrl { get; set; } = null!;
    
    // TODO include article up votes and comment counts
}

public class HomeArticleTag(int id, string name)
{
    public int TagId { get; private set; } = id;
    public string TagName { get; private set; } = name;
}