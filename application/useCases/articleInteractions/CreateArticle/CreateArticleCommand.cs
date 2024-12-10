using domain.entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace application.useCases.articleInteractions.CreateArticle;

public class CreateArticleCommand : IRequest<CreateArticleResponse>
{
    public int ClubId { get; set; }
    public string ArticleTitle { get; set; } = null!;
    public string ArticleHtmlContent { get; set; } = null!;
    public List<int>? TagIds { get; set; }
    public List<string>? NewTags { get; set; }
    public string ArticleContent { get; set; } = null!;
    public bool IsDrafted { get; set; } = false;
    public IFormFile? ArticleThumbnail { get; set; }
    
}

public class CreateArticleResponse
{
    public string Message { get; set; } = null!;
    public int ArticleId { get; set; }
}