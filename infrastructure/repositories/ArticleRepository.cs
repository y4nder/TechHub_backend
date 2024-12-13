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
            .Where(a => 
                a.Tags.Any(tag => tagIds.Contains(tag.TagId)) && 
                !a.Archived && 
                a.ArticleAuthorId != userId 
                && !a.Club!.Private
                && !a.IsDrafted)
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
            .Where(a => a.Archived == false && !a.Club!.Private && !a.IsDrafted)
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
                    article.NormalizedArticleTitle, $"%{normalizedQuery}%")&& !article.IsDrafted);

        return await GetPaginatedArticleCardExecutor(baseQuery, userId, pageNumber, pageSize);
    }
    
    

    public async Task<PaginatedResult<ArticleResponseDto>> GetPaginatedArticlesByClubIdAsync(int clubId, int pageNumber,
        int pageSize, int userId)
    {
        var baseQuery = _context.Articles
            .Where(article => article.ClubId == clubId && !article.Archived && !article.IsDrafted);
        
        return await GetPaginatedArticleCardExecutor(baseQuery, userId, pageNumber, pageSize);
    }

    public async Task<PaginatedResult<ArticleResponseDto>> GetPaginatedArticlesByUserId(
        int authorId, int pageNumber, int pageSize)
    {
        var baseQuery = _context.Articles
            .Where(article => article.ArticleAuthorId == authorId && !article.Archived && !article.IsDrafted);
        
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

    public async Task<PaginatedResult<ArticleResponseDto>> GetPaginatedArticlesByTagId(int userId, int tagId, int pageNumber, int pageSize)
    {
        var baseQuery = _context.Articles
            .AsNoTracking()
            .Where(article => article.Tags.Any(tag => tag.TagId == tagId) && !article.Archived && !article.IsDrafted)
            .OrderByDescending(article => article.CreatedDateTime);
        
        return await GetPaginatedArticleCardExecutor(baseQuery, userId, pageNumber, pageSize);
    }

    public async Task<bool> IsAuthor(int userId, int articleId)
    {
        return await _context
            .Articles
            .AnyAsync(article => 
                article.ArticleId == articleId 
                && article.ArticleAuthorId == userId);  ; 
    }

    public async Task<ArticleResponseForEditDto> GetArticleForEditByIdAsync(int articleId)
    {
        return await _context.Articles
            .Where(article => article.ArticleId == articleId)
            .Select(article => new ArticleResponseForEditDto
            {
                ArticleId = article.ArticleId,
                ClubId = article.ClubId,
                ArticleTitle = article.ArticleTitle,
                ArticleThumbnail = article.ArticleThumbnailUrl!,
                ArticleContent = _context.ArticleBodies.Where(body => body.ArticleId == article.ArticleId)
                    .Select(body => body.ArticleContent).FirstOrDefault()!,
                Tags = article.Tags.Select(t => new TagDto
                {
                    TagId = t.TagId,
                    TagName = t.TagName
                }).ToList(),
            }).FirstAsync();
    }

    public async Task<List<ArticleSuggestionDto>> GetArticleSuggestions(string searchQuery)
    {
        if (string.IsNullOrWhiteSpace(searchQuery))
            return new ();
        
        searchQuery = searchQuery.ToUpper();

        return await _context.Articles
            .AsNoTracking()
            .Where(a => EF.Functions.Like(a.NormalizedArticleTitle, $"{searchQuery}%") && !a.Archived && !a.IsDrafted)
            .Take(3)
            .Select(t => new ArticleSuggestionDto
            {
                ArticleId = t.ArticleId,
                ArticleTitle = t.ArticleTitle,
            })
            .ToListAsync();
    }


    // private async Task<PaginatedResult<ArticleResponseDto>> GetPaginatedArticleCardExecutor(
    //     IQueryable<Article> baseQuery, int userId, int pageNumber, int pageSize)
    // {
    //     // Calculate the total count for pagination
    //     var totalCount = await baseQuery.CountAsync();
    //
    //     // Project and paginate the base query
    //     var articles = await baseQuery
    //         .OrderByDescending(article => article.CreatedDateTime)
    //         .Skip((pageNumber - 1) * pageSize)
    //         .Take(pageSize)
    //         .Select(article => new ArticleResponseDto
    //         {
    //             ArticleId = article.ArticleId,
    //             ClubImageUrl = article.Club!.ClubImageUrl ?? string.Empty,
    //             AuthorId = article.ArticleAuthorId,
    //             AuthorName = article.ArticleAuthor!.Username!,
    //             UserImageUrl = article.ArticleAuthor!.UserProfilePicUrl,
    //             ClubId = article.ClubId,
    //             ClubName = article.Club.ClubName?? string.Empty,
    //             ArticleTitle = article.ArticleTitle,
    //             Tags = article.Tags
    //                 .Select(tag => new TagDto
    //                 {
    //                     TagId = tag.TagId,
    //                     TagName = tag.TagName ?? "Unknown Tag"
    //                 })
    //                 .ToList(),
    //             CreatedDateTime = article.CreatedDateTime,
    //             ArticleThumbnailUrl = article.ArticleThumbnailUrl ?? string.Empty,
    //             VoteCount = article.UserArticleVotes.Sum(vote => vote.VoteType),
    //             CommentCount = article.Comments.Count(),
    //             VoteType = _context.UserArticleVotes
    //                 .Where(v => v.UserId == userId && v.ArticleId == article.ArticleId)
    //                 .Select(v => v.VoteType).FirstOrDefault(),
    //             Bookmarked = _context.UserArticleBookmarks
    //                 .Any(b => b.ArticleId == article.ArticleId && b.UserId == userId)
    //         })
    //         .ToListAsync();
    //
    //     // Return the paginated result
    //     return new PaginatedResult<ArticleResponseDto>
    //     {
    //         Items = articles,
    //         TotalCount = totalCount,
    //         PageNumber = pageNumber,
    //         PageSize = pageSize,
    //         TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
    //     };
    // }
    //
    private async Task<PaginatedResult<ArticleResponseDto>> GetPaginatedArticleCardExecutor(
    IQueryable<Article> baseQuery, int userId, int pageNumber, int pageSize)
{
    // Calculate the total count for pagination
    var totalCount = await baseQuery.CountAsync();

    // Project and paginate the base query
    var articles = await baseQuery
        .AsNoTracking()
        .Include(article => article.Club)
        .Include(article => article.ArticleAuthor)
        .Include(article => article.Tags)
        .Include(article => article.UserArticleVotes)
        .Include(article => article.Comments)
        .OrderByDescending(article => article.CreatedDateTime)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .Select(article => new ArticleResponseDto
        {
            ArticleId = article.ArticleId,
            ClubImageUrl = article.Club!.ClubImageUrl ?? string.Empty,
            AuthorId = article.ArticleAuthorId,
            AuthorName = article.ArticleAuthor!.Username!,
            UserImageUrl = article.ArticleAuthor!.UserProfilePicUrl,
            ClubId = article.ClubId,
            ClubName = article.Club!.ClubName ?? string.Empty,
            ArticleTitle = article.ArticleTitle,
            Tags = article.Tags.Select(tag => new TagDto
            {
                TagId = tag.TagId,
                TagName = tag.TagName ?? "Unknown Tag"
            }).ToList(),
            CreatedDateTime = article.CreatedDateTime,
            ArticleThumbnailUrl = article.ArticleThumbnailUrl ?? string.Empty,
            VoteCount = article.UserArticleVotes.Sum(vote => vote.VoteType),
            CommentCount = article.Comments.Count(),
            VoteType = article.UserArticleVotes
                .Where(v => v.UserId == userId)
                .Select(v => v.VoteType)
                .FirstOrDefault(),
            Bookmarked = article.UserArticleBookmarks
                .Any(b => b.UserId == userId)
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