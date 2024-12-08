namespace domain.entities;

public partial class ArticleBody
{
    public int ArticleId { get; set; }

    public string ArticleContent { get; set; } = null!;
    public string ArticleHtmlContent { get; set; } = null!;

    public virtual Article Article { get; set; } = null!;

    public static ArticleBody Create(int articleId, string articleContent, string articleHtmlContent)
    {
        return new ArticleBody
        {
            ArticleId = articleId,
            ArticleContent = articleContent,
            ArticleHtmlContent = articleHtmlContent
        };
    }
}

public class ArticleBodyDto
{
    public string ArticleContent { get; set; } = null!;
    public string ArticleHtmlContent { get; set; } = null!;
}

