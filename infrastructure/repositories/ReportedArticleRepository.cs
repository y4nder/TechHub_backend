using domain.entities;
using domain.interfaces;
using domain.pagination;
using Microsoft.EntityFrameworkCore;

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

    public async Task<PaginatedResult<ReportedArticleDtoResponse>> GetReportedArticlesByClubId(int clubId, int pageNumber, int pageSize)
    {
        var baseQuery = _context.ReportedArticles
            .Where(r => 
                r.Article.ClubId == clubId && 
                !r.Evaluated)
            .AsEnumerable() // Forces the query to execute in memory
            .GroupBy(r => r.ArticleId)
            .Select(group => group
                .OrderByDescending(r => r.ReportDateTime)
                .FirstOrDefault())
            .Where(r => r != null) // Exclude null values
            .AsQueryable(); // Convert back to IQueryable for further operations

        return await GetReportedArticlesPaginated(baseQuery!, clubId, pageNumber, pageSize);
    }

    public async Task<List<ReportedArticleDtoResponse>> GetReportedArticlesByClubId(int clubId)
    {
        // Load all reported articles into memory
        var allReports = await _context.ReportedArticles
            .Where(r => r.Article.ClubId == clubId && !r.Evaluated)
            .Include(r => r.Article)
            .ThenInclude(a => a.ArticleAuthor)
            .AsNoTracking()
            .ToListAsync();

        // Group by ArticleId and select the most recent report for each article
        var uniqueReports = allReports
            .GroupBy(r => r.ArticleId)
            .Select(g => g.OrderByDescending(r => r.ReportDateTime).First())
            .Select(r => new ReportedArticleDtoResponse
            {
                ArticleId = r.ArticleId,
                ArticleTitle = r.Article.ArticleTitle,
                ReporterId = r.ReporterId,
                ReportReason = r.ReportReason,
                AdditionalNotes = r.AdditionalNotes,
                UserProfilePicUrl = r.Article.ArticleAuthor!.UserProfilePicUrl,
                Evaluated = r.Evaluated
            })
            .ToList();

        return uniqueReports;
    }




    public async Task<List<ReportedArticleDtoResponse>> GetReportedArticlesBySize(int clubId, int size = 10)
    {
        return await _context.ReportedArticles
            .Where(r => 
                r.Article.ClubId == clubId && 
                !r.Evaluated)
            .OrderByDescending(r => r.ReportDateTime)
            .Include(r => r.Article)
            .ThenInclude(r => r.ArticleAuthor)
            .AsNoTracking()
            .Take(size)
            .Select(r => new ReportedArticleDtoResponse
            {
                AdditionalNotes = r.AdditionalNotes,
                ArticleId = r.ArticleId,
                ArticleTitle = r.Article.ArticleTitle,
                UserProfilePicUrl = r.Article.ArticleAuthor!.UserProfilePicUrl,
                Evaluated = r.Evaluated,
                ReporterId = r.ReporterId,
                ReportReason = r.ReportReason,
            })
            .ToListAsync();
    }

    public async Task<List<ReportResponseMinimal>> GetReportsForArticle(int articleId)
    {
        return await _context.ReportedArticles
            .Where(r => r.ArticleId == articleId)
            .AsNoTracking()
            .Select(r => new ReportResponseMinimal
            {
                ReportReason = r.ReportReason,
                AdditionalNotes = r.AdditionalNotes,
            })
            .ToListAsync();
    }

    public async Task<List<ReportedArticle>> GetArticleReportRecords(int articleId)
    {
        return await _context.ReportedArticles
            .Where(r => r.ArticleId == articleId)
            .ToListAsync();
    }

    public async Task<bool> HasReportedRecord(int articleId, int userId)
    {
        return await _context.ReportedArticles
            .AnyAsync(r => 
                r.ArticleId == articleId && 
                r.ReporterId == userId &&
                !r.Evaluated); 
    }

    private async Task<PaginatedResult<ReportedArticleDtoResponse>> GetReportedArticlesPaginated(
        IQueryable<ReportedArticle> baseQuery,int clubId, int pageNumber, int pageSize)
    {
        var totalCount = await baseQuery.CountAsync();
        
        var reportedArticles = await baseQuery
            .Include(r => r.Article)
            .ThenInclude(r => r.ArticleAuthor)
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(r => new ReportedArticleDtoResponse
            {
                AdditionalNotes = r.AdditionalNotes,
                ArticleId = r.ArticleId,
                ArticleTitle = r.Article.ArticleTitle,
                UserProfilePicUrl = r.Article.ArticleAuthor!.UserProfilePicUrl,
                Evaluated = r.Evaluated,
                ReporterId = r.ReporterId,
                ReportReason = r.ReportReason,
            })
            .ToListAsync();
        
        return new PaginatedResult<ReportedArticleDtoResponse>
        {
            Items = reportedArticles,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }
    
    
}