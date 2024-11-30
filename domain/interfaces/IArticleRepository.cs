using domain.entities;
using domain.pagination;

namespace domain.interfaces;

public interface IArticleRepository
{
    void AddArticle(Article article);
    
    Task<Article?> GetArticleByIdAsync(int articleId);
    void Update(Article article);
    
    Task<bool> ArticleExistsAsync(int articleId);
    
    Task<bool> ArticleExistsByIdIgnoreArchived(int articleId);

    Task<Article?> GetArticleByIdNoTracking(int articleId);
    Task<PaginatedResult<ArticleResponseDto>> GetPaginatedHomeArticlesByTagIdsAsync(int userId, List<int> tagIds,
        int pageNumber, int pageSize);
    Task<Article?> QuerySingleArticleByIdAsync(int articleId);
    
    Task<PaginatedResult<ArticleResponseDto>> GetPaginatedDiscoverArticlesAsync(int userId, int pageNumber,
        int pageSize);

    Task<PaginatedResult<ArticleResponseDto>> GetPaginatedArticlesBySearchQueryAsync(string searchQuery, int userId,
        int pageNumber, int pageSize);

    Task<PaginatedResult<ArticleResponseDto>> GetPaginatedArticlesByClubIdAsync(int clubId, int pageNumber,
        int pageSize, int userId);
    
    Task<PaginatedResult<ArticleResponseDto>> GetPaginatedArticlesByUserId(int authorId, int pageNumber, int pageSize);
    
    
}