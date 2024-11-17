using domain.entities;

namespace domain.interfaces;

public interface IArticleBodyRepository
{
    void AddArticleBody(ArticleBody articleBody);
    
    Task<ArticleBody?> GetArticleBodyByIdAsync(int articleId);
}