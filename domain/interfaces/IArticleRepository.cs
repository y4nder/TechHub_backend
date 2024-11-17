using domain.entities;

namespace domain.interfaces;

public interface IArticleRepository
{
    void AddArticle(Article article);
    
    Task<Article?> GetArticleByIdAsync(int articleId);
    void Update(Article article);
    
    Task<bool> ArticleExistsAsync(int articleId);

    Task<Article?> GetArticleByIdNoTracking(int articleId);
}