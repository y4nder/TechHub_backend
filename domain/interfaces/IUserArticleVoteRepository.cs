using domain.entities;

namespace domain.interfaces;

public interface IUserArticleVoteRepository
{
    void AddUserArticleVote(UserArticleVote userArticleVote);
    
    Task<bool> CheckAlreadyVoted(UserArticleVote userArticleVote);
    
    Task<UserArticleVote?> GetUserArticleVoteByRecord(UserArticleVote userArticleVote);
    
    Task<int> GetArticleVoteCount(int articleId);
    
    Task RemoveUserArticleVote(UserArticleVote userArticleVote);

    Task<short> GetArticleVoteType(int articleId, int userId);
}