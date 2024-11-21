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

    public async Task<PaginatedResult<ArticleResponseDto>> GetPaginatedHomeArticlesByTagIdsAsync(List<int> tagIds, int pageNumber,
        int pageSize)
    {
        var baseQuery = _context.Articles
            .AsNoTracking()
            .Where(a => a.Tags.Any(tag => tagIds.Contains(tag.TagId)) && !a.Archived)
            .OrderBy(a => a.CreatedDateTime); 
        
        return await GetPaginatedArticleCardExecutor(baseQuery, pageNumber, pageSize);
    }

    public async Task<Article?> QuerySingleArticleByIdAsync(int articleId)
    {
        return await _context.Articles
            .AsNoTracking()
            .Include(a => a.Tags)
            .Include(a => a.ArticleAuthor)
            .Where(a => a.ArticleId == articleId).FirstOrDefaultAsync();
    }

    
    public async Task<PaginatedResult<ArticleResponseDto>> GetPaginatedArticlesBySearchQueryAsync(
        string searchQuery, int pageNumber, int pageSize)
    {
        var normalizedQuery = searchQuery.ToUpper();

        var baseQuery = _context.Articles
            .Where(article => 
                EF.Functions.Like(
                    article.NormalizedArticleTitle, $"%{normalizedQuery}%"));

        return await GetPaginatedArticleCardExecutor(baseQuery, pageNumber, pageSize);
    }

    public async Task<PaginatedResult<ArticleResponseDto>> GetPaginatedArticlesByClubIdAsync(
        int clubId, int pageNumber, int pageSize)
    {
        var baseQuery = _context.Articles
            .Where(article => article.ClubId == clubId);
        
        return await GetPaginatedArticleCardExecutor(baseQuery, pageNumber, pageSize);
    }

    public async Task<PaginatedResult<ArticleResponseDto>> GetPaginatedArticlesByUserId(
        int authorId, int pageNumber, int pageSize)
    {
        var baseQuery = _context.Articles
            .Where(article => article.ArticleAuthorId == authorId);
        
        return await GetPaginatedArticleCardExecutor(baseQuery, pageNumber, pageSize);
    }
    
    private async Task<PaginatedResult<ArticleResponseDto>> GetPaginatedArticleCardExecutor(
        IQueryable<Article> baseQuery, int pageNumber, int pageSize)
    {
        var projectedQuery = baseQuery
            .OrderBy(article => article.CreatedDateTime)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(article => new ArticleResponseDto
            {
                ArticleId = article.ArticleId,
                ClubImageUrl = article.Club != null ? article.Club.ClubImageUrl! : string.Empty,
                UserImageUrl = article.ArticleAuthor != null ? article.ArticleAuthor.UserProfilePicUrl! : string.Empty,
                ArticleTitle = article.ArticleTitle ?? "Untitled",
                Tags = article.Tags.Select(tag => new TagDto
                {
                    TagId = tag.TagId,
                    TagName = tag.TagName ?? "Unknown Tag"
                }).ToList(),
                CreatedDateTime = article.CreatedDateTime,
                ArticleThumbnailUrl = article.ArticleThumbnailUrl ?? string.Empty,
                VoteCount = _context.UserArticleVotes
                    .Where(vote => vote.ArticleId == article.ArticleId)
                    .Sum(vote => vote.VoteType),
                CommentCount = _context.Comments.Count(comment => comment.ArticleId == article.ArticleId)
            });

        // Get paginated results and total count in parallel
        var totalCountTask = baseQuery.CountAsync();
        var articlesTask = projectedQuery.ToListAsync();

        await Task.WhenAll(totalCountTask, articlesTask);

        return new PaginatedResult<ArticleResponseDto>
        {
            Items = articlesTask.Result,
            TotalCount = totalCountTask.Result,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalCountTask.Result / (double)pageSize)
        };
    }
    
}