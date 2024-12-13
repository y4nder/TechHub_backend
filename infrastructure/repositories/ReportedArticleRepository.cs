using domain.entities;
using domain.interfaces;

namespace infrastructure.repositories;

public class ReportedArticleRepository : IReportedArticleRepository
{
    private readonly AppDbContext _context;

    public ReportedArticleRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddReportedArticle(ReportedArticle reportedArticle)
    {
        _context.ReportedArticles.Add(reportedArticle);
    }
}