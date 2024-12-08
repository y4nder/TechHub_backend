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

    public async Task<PaginatedResult<ArticleResponseDto>> GetPaginatedHomeArticlesByTagIdsAsync(int userId,
        List<int> tagIds, int pageNumber,
        int pageSize)
    {
        var baseQuery = _context.Articles
            .AsNoTracking()
            .Include(a => a.Club)
            .Where(a => a.Tags.Any(tag => tagIds.Contains(tag.TagId)) && !a.Archived && a.ArticleAuthorId != userId && !a.Club!.Private)
            .OrderBy(a => a.CreatedDateTime); 
        
        return await GetPaginatedArticleCardExecutor(baseQuery, userId, pageNumber, pageSize);
    }

    public async Task<Article?> QuerySingleArticleByIdAsync(int articleId)
    {
        return await _context.Articles
            .AsNoTracking()
            .Include(a => a.Tags)
            .Include(a => a.ArticleAuthor)
            .ThenInclude(a => a.UserAdditionalInfo)
            .Where(a => a.ArticleId == articleId).FirstOrDefaultAsync();
    }

    public async Task<PaginatedResult<ArticleResponseDto>> GetPaginatedDiscoverArticlesAsync(int userId, int pageNumber,
        int pageSize)
    {
        var baseQuery = _context.Articles
            .AsNoTracking()
            .Include(a => a.Club)
            .Where(a => a.Archived == false && !a.Club!.Private)
            .OrderBy(a => a.CreatedDateTime);
        
        return await GetPaginatedArticleCardExecutor(baseQuery, userId, pageNumber, pageSize);
    }


    public async Task<PaginatedResult<ArticleResponseDto>> GetPaginatedArticlesBySearchQueryAsync(string searchQuery,
        int userId, int pageNumber, int pageSize)
    {
        var normalizedQuery = searchQuery.ToUpper();

        var baseQuery = _context.Articles
            .Where(article => 
                EF.Functions.Like(
                    article.NormalizedArticleTitle, $"%{normalizedQuery}%"));

        return await GetPaginatedArticleCardExecutor(baseQuery, userId, pageNumber, pageSize);
    }

    public async Task<PaginatedResult<ArticleResponseDto>> GetPaginatedArticlesByClubIdAsync(int clubId, int pageNumber,
        int pageSize, int userId)
    {
        var baseQuery = _context.Articles
            .Where(article => article.ClubId == clubId && !article.Archived);
        
        return await GetPaginatedArticleCardExecutor(baseQuery, userId, pageNumber, pageSize);
    }

    public async Task<PaginatedResult<ArticleResponseDto>> GetPaginatedArticlesByUserId(
        int authorId, int pageNumber, int pageSize)
    {
        var baseQuery = _context.Articles
            .Where(article => article.ArticleAuthorId == authorId && !article.Archived);
        
        return await GetPaginatedArticleCardExecutor(baseQuery, authorId, pageNumber, pageSize);
    }

    public async Task<PaginatedResult<ArticleResponseDto>> GetPaginatedUpVotedArticles(int userId, int pageNumber, int pageSize)
    {
        var baseQuery = _context.Articles
            .AsNoTracking()
            .Where(article => article.UserArticleVotes.Any(vote => vote.UserId == userId && vote.VoteType == 1))
            .OrderByDescending(article => article.CreatedDateTime);
        
        return await GetPaginatedArticleCardExecutor(baseQuery, userId, pageNumber, pageSize);
    }

    public async Task<PaginatedResult<ArticleResponseDto>> GetPaginatedBookmarkedArticles(int userId, int pageNumber, int pageSize)
    {
        var baseQuery = _context.Articles
            .AsNoTracking()
            .Where(article => article.UserArticleBookmarks.Any(bookmark => bookmark.UserId == userId))
            .OrderByDescending(article => article.CreatedDateTime);
        
        return await GetPaginatedArticleCardExecutor(baseQuery, userId, pageNumber, pageSize); 
    }

    public async Task<PaginatedResult<ArticleResponseDto>> GetPaginatedReadArticles(int userId, int pageNumber, int pageSize)
    {
        var baseQuery = _context.Articles
            .AsNoTracking()
            .Where(article => article.UserArticleReads.Any(read => read.UserId == userId) && !article.Archived)
            .OrderByDescending(article => article.CreatedDateTime);
        
        return await GetPaginatedArticleCardExecutor(baseQuery, userId, pageNumber, pageSize);
    }


    private async Task<PaginatedResult<ArticleResponseDto>> GetPaginatedArticleCardExecutor(
        IQueryable<Article> baseQuery, int userId, int pageNumber, int pageSize)
    {
        // Calculate the total count for pagination
        var totalCount = await baseQuery.CountAsync();

        // Project and paginate the base query
        var articles = await baseQuery
            .OrderByDescending(article => article.CreatedDateTime)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(article => new ArticleResponseDto
            {
                ArticleId = article.ArticleId,
                ClubImageUrl = article.Club!.ClubImageUrl ?? string.Empty,
                UserImageUrl = article.ArticleAuthor!.UserProfilePicUrl,
                ClubName = article.Club.ClubName?? string.Empty,
                ArticleTitle = article.ArticleTitle,
                Tags = article.Tags
                    .Select(tag => new TagDto
                    {
                        TagId = tag.TagId,
                        TagName = tag.TagName ?? "Unknown Tag"
                    })
                    .ToList(),
                CreatedDateTime = article.CreatedDateTime,
                ArticleThumbnailUrl = article.ArticleThumbnailUrl ?? string.Empty,
                VoteCount = article.UserArticleVotes.Sum(vote => vote.VoteType),
                CommentCount = article.Comments.Count(),
                VoteType = _context.UserArticleVotes
                    .Where(v => v.UserId == userId && v.ArticleId == article.ArticleId)
                    .Select(v => v.VoteType).FirstOrDefault(),
                Bookmarked = _context.UserArticleBookmarks
                    .Any(b => b.ArticleId == article.ArticleId && b.UserId == userId)
            })
            .ToListAsync();

        // Return the paginated result
        return new PaginatedResult<ArticleResponseDto>
        {
            Items = articles,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }
}