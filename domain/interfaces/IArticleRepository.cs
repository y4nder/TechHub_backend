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
    Task<PaginatedResult<HomeArticle>> GetPaginatedHomeArticlesByTagIdsAsync(List<int> tagIds, int pageNumber, int pageSize);
    Task<Article?> QuerySingleArticleByIdAsync(int articleId);

    Task<PaginatedResult<HomeArticle>> GetArticlesBySearchQueryAsync(string searchQuery, int pageNumber, int pageSize);

    IQueryable<Article> GetArticleQueryable();
    
    
}