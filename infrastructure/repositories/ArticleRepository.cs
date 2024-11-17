using domain.entities;
using domain.interfaces;
using domain.pagination;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly AppDbContext _context;

    public ArticleRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddArticle(Article article)
    {
        _context.Articles.Add(article);
    }

    public async Task<Article?> GetArticleByIdAsync(int articleId)
    {
        return await _context.Articles.FindAsync(articleId);
    }

    public void Update(Article article)
    {
        _context.Entry(article).State = EntityState.Modified;
    }

    public async Task<bool> ArticleExistsAsync(int articleId)
    {
        return await _context.Articles
            .AsNoTracking()
            .AnyAsync(a => a.ArticleId == articleId);
    }

    public async Task<Article?> GetArticleByIdNoTracking(int articleId)
    {
        return await  _context.Articles
            .AsNoTracking()
            .Where(a => a.ArticleId == articleId)
            .FirstOrDefaultAsync(); 
    }

    public async Task<PaginatedResult<Article>> GetPaginatedArticlesByTagIdsAsync(List<int> tagIds, int pageNumber, int pageSize)
    {
        var query = _context.Articles
            .AsNoTracking()
            .Include(a => a.Club)
            .Include(a => a.Tags)
            .Where(a => a.Tags.Any(tag => tagIds.Contains(tag.TagId)) && !a.Archived)
            .OrderBy(a => a.CreatedDateTime); 

        var totalCount = await query.CountAsync(); 

        var articles = await query
            .Skip((pageNumber - 1) * pageSize) 
            .Take(pageSize)                   
            .ToListAsync();

        return new PaginatedResult<Article>
        {
            Items = articles,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }

    public async Task<Article?> QuerySingleArticleByIdAsync(int articleId)
    {
        return await _context.Articles
            .AsNoTracking()
            .Include(a => a.Tags)
            .Include(a => a.ArticleAuthor)
            .Where(a => a.ArticleId == articleId).FirstOrDefaultAsync();
    }
}