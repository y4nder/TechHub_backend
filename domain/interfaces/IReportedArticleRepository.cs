using domain.entities;

namespace domain.interfaces;

public interface IReportedArticleRepository
{
    void AddReportedArticle(ReportedArticle reportedArticle);
}