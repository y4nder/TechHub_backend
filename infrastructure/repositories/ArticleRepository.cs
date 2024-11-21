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
        var query = _context.Articles.AsQueryable();
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

    public async Task<bool> ArticleExistsByIdIgnoreArchived(int articleId)
    {
        return await _context.Articles
            .AsNoTracking()
            .AnyAsync(a => a.ArticleId == articleId && a.Archived == false);
    }

    public async Task<Article?> GetArticleByIdNoTracking(int articleId)
    {
        return await  _context.Articles
            .AsNoTracking()
            .Where(a => a.ArticleId == articleId)
            .FirstOrDefaultAsync(); 
    }

    public async Task<PaginatedResult<HomeArticle>> GetPaginatedHomeArticlesByTagIdsAsync(List<int> tagIds, int pageNumber,
        int pageSize)
    {
        var query = _context.Articles
            .AsNoTracking()
            .Include(a => a.Club)
            .Include(a => a.ArticleAuthor)
            .Include(a => a.Tags)
            .Where(a => a.Tags.Any(tag => tagIds.Contains(tag.TagId)) && !a.Archived)
            .OrderBy(a => a.CreatedDateTime); 

        var totalCount = await query.CountAsync(); 

        var homeArticles = await query
            .Skip((pageNumber - 1) * pageSize) 
            .Take(pageSize)                   
            .Select(article => new HomeArticle
            {
                ArticleId = article.ArticleId,
                ClubImageUrl = article.Club!.ClubImageUrl!,
                UserImageUrl = article.ArticleAuthor!.UserProfilePicUrl,
                ArticleTitle = article.ArticleTitle,
                Tags = article.Tags.Select(tag => new TagDto
                {
                    TagId = tag.TagId,
                    TagName = tag.TagName
                }).ToList(),
                CreatedDateTime = article.CreatedDateTime,
                ArticleThumbnailUrl = article.ArticleThumbnailUrl!
            })
            .ToListAsync();

        return new PaginatedResult<HomeArticle>
        {
            Items = homeArticles,
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

    public async Task<PaginatedResult<HomeArticle>> GetArticlesBySearchQueryAsync(string searchQuery, int pageNumber,
        int pageSize)
    {
        var normalizedQuery = searchQuery.ToUpper();
        
        var query = _context.Articles.AsQueryable();
        
        query = query
            .Where(article => 
                EF.Functions
                    .Like(article.NormalizedArticleTitle, $"%{normalizedQuery}%"))
            .OrderBy(article => article.CreatedDateTime);
        
        var totalCount = await query.CountAsync();

        var dtoQuery = query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(article => new HomeArticle
            {
                ArticleId = article.ArticleId,
                ClubImageUrl = article.Club!.ClubImageUrl!,
                UserImageUrl = article.ArticleAuthor!.UserProfilePicUrl,
                ArticleTitle = article.ArticleTitle,
                Tags = article.Tags.Select(tag => new TagDto
                {
                    TagId = tag.TagId,
                    TagName = tag.TagName
                }).ToList(),
                CreatedDateTime = article.CreatedDateTime,
                ArticleThumbnailUrl = article.ArticleThumbnailUrl!
            });

        var homeArticles = await dtoQuery.ToListAsync();

        return new PaginatedResult<HomeArticle>
        {
            Items = homeArticles,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };

    }

    public IQueryable<Article> GetArticleQueryable()
    {
        return _context.Articles.AsQueryable();
    }
    
    
}