using domain.entities;

namespace domain.interfaces;

public interface IArticleBodyRepository
{
    void AddArticleBody(ArticleBody articleBody);
    
    Task<ArticleBody?> GetArticleBodyByIdAsync(int articleId);
    
    Task<ArticleBodyDto?> GetArticleBodyDtoByIdAsync(int articleId);
    Task<string?> GetArticleHtmlContentByIdAsync(int articleId);
}