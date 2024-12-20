using domain.shared;

namespace domain.entities;

public class ReportedArticle
{
    public int ReportId { get; set; }
    public int ReporterId { get; set; }
    public int ArticleId { get; set; }
    public string ReportReason { get; set; } = null!;
    public string AdditionalNotes { get; set; } = "";
    public DateTime ReportDateTime { get; set; } = DateTime.Now;
    public User Reporter { get; set; } = null!;
    public Article Article { get; set; } = null!;
    public bool Evaluated { get; set; } = false;
    public string EvaluationNotes { get; set; } = "";

    public static ReportedArticle Create(ReportArticleDto reportArticleDto)
    {
        return new ReportedArticle
        {
            ReporterId = reportArticleDto.ReporterId,
            ArticleId = reportArticleDto.ArticleId,
            ReportReason = reportArticleDto.ReportReason,
            AdditionalNotes = reportArticleDto.AdditionalNotes,
            Evaluated = false
        };
    }

    public void Evaluate(string evaluationNotes)
    {
        EvaluationNotes = Updater.UpdateProperty(EvaluationNotes, evaluationNotes );
        Evaluated = true;
    }
}

public enum ReportAction
{
    NoAction, RemoveArticle, BanUser
} 

public static class ReportType
{
    public static bool IsValidEnum(int value)
    {
        // Check if the value is a valid member of the enum
        return Enum.IsDefined(typeof(ReportAction), value);
    }
}

public class ReportArticleDto
{
    public int ReporterId { get; set; }
    public int ArticleId { get; set; }
    public string ReportReason { get; set; } = null!;
    public string AdditionalNotes { get; set; } = "";
}

public class ReportedArticleDtoResponse
{
    public int ReporterId { get; set; }
    public int ArticleId { get; set; }
    public string ArticleTitle { get; set; } = null!;
    public string UserProfilePicUrl { get; set; } = null!;
    public string ReportReason { get; set; } = null!;
    public string AdditionalNotes { get; set; } = "";
    public bool Evaluated { get; set; } 
}

public class ReportResponseMinimal
{
    public string ReportReason { get; set; } = null!;
    public string AdditionalNotes { get; set; } = "";
}

