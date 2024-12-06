using domain.entities;

namespace domain.interfaces;

public interface IUserArticleReadRepository
{
    void AddReadHistory(UserArticleRead userArticleRead);
    
    Task<List<UserArticleRead>?> GetUserReadHistories(int userId);

    Task<UserArticleRead?> GetUserArticleRead(int userId, int articleId);
    
}