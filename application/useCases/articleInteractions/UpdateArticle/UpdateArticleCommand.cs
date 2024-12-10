using MediatR;
using Microsoft.AspNetCore.Http;

namespace application.useCases.articleInteractions.UpdateArticle;

public class UpdateArticleCommand : IRequest<UpdateArticleResponse>
{
    public int ArticleId { get; set; }
    public int ClubId { get; set; }
    public string ArticleTitle { get; set; } = null!;
    public string ArticleHtmlContent { get; set; } = null!;
    public List<int>? TagIds { get; set; }
    public List<string>? NewTags { get; set; }
    public string ArticleContent { get; set; } = null!;
    public bool IsDrafted { get; set; } = false;
    public IFormFile? ArticleThumbnail { get; set; }
    public string ArticleThumbnailUrl { get; set; } = null!;
}

public class UpdateArticleResponse
{
    public string Message { get; set; } = null!;
    public int ArticleId { get; set; }
}