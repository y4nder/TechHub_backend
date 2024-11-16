using MediatR;
using Microsoft.AspNetCore.Http;

namespace application.useCases.articleInteractions.CreateArticle;

public class CreateArticleCommand : IRequest<CreateArticleResponse>
{
    public int AuthorId { get; set; }
    public int ClubId { get; set; }
    public string ArticleTitle { get; set; } = null!;
    public IFormFile? ArticleThumbnail { get; set; }
    public List<int>? TagIds { get; set; }
    public string ArticleContent { get; set; } = null!;
    public bool IsDrafted { get; set; } = false;
}

public class CreateArticleResponse
{
    public string Message { get; set; } = null!;
}