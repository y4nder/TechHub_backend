using domain.entities;
using domain.pagination;

namespace domain.interfaces;

public interface ICommentRepository
{
    void AddComment(Comment comment);
    
    Task<bool> ParentCommentIdExists(int parentCommentId);
    
    Task<PaginatedResult<ArticleCommentDto>> GetPaginatedCommentsByArticleId(int userId, int articleId, int pageNumber,
        int pageSize);
    
    Task<int> GetTotalCommentsByArticleId(int articleId);
    
    Task<bool> CheckCommentIdExists(int commentId);
    
    Task<PaginatedResult<UserReplyDto>> GetUserReplies(int userId, int pageNumber, int pageSize);
}