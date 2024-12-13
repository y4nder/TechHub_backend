namespace domain.entities;

public class ReportedArticle
{
    public int ReportId { get; set; }
    public string ReportReason { get; set; } = null!;
    public string AdditionalNotes { get; set; } = "";
    public DateTime ReportDateTime { get; set; } = DateTime.Now;
    public int ReporterId { get; set; }
    public User Reporter { get; set; } = null!;
    public int ArticleId { get; set; }
    public Article Article { get; set; } = null!;
    public bool Evaluated { get; set; } = false;

    public static ReportedArticle Create(ReportArticleDto reportArticleDto)
    {
        return new ReportedArticle
        {
            ReportReason = reportArticleDto.ReportReason,
            AdditionalNotes = reportArticleDto.AdditionalNotes,
            ReporterId = reportArticleDto.ReporterId,
            ArticleId = reportArticleDto.ArticleId,
        };
    }
}

public class ReportArticleDto
{
    public string ReportReason { get; set; } = null!;
    public string AdditionalNotes { get; set; } = "";
    public int ReporterId { get; set; }
    public int ArticleId { get; set; }
}


