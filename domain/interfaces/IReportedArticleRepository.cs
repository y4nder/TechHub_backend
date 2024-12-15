using domain.entities;
using domain.pagination;

namespace domain.interfaces;

public interface IReportedArticleRepository
{
    void AddReportedArticle(ReportedArticle reportedArticle);
    
    Task<PaginatedResult<ReportedArticleDtoResponse>> GetReportedArticlesByClubId(int clubId, int pageNumber, int pageSize);
    
    Task<List<ReportedArticleDtoResponse>> GetReportedArticlesByClubId(int clubId);
    
    Task<List<ReportedArticleDtoResponse>> GetReportedArticlesBySize(int clubId, int size = 10);

    Task<List<ReportResponseMinimal>> GetReportsForArticle(int articleId);
    
    Task<List<ReportedArticle>> GetArticleReportRecords(int articleId);
    
    Task<bool> HasReportedRecord(int articleId, int userId);
}