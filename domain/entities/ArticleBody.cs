namespace domain.entities;

public partial class ArticleBody
{
    public int? ArticleId { get; set; }

    public string? ArticleContent { get; set; }

    public virtual Article? Article { get; set; }
}
