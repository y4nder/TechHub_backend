using domain.entities;
using domain.pagination;

namespace domain.interfaces;

public interface ICommentRepository
{
    void AddComment(Comment comment);
    
    Task<bool> ParentCommentIdExists(int parentCommentId);
    
    Task<PaginatedResult<ArticleCommentDto>> GetPaginatedCommentsByArticleId(int articleId, int pageNumber,
        int pageSize);
    
    Task<bool> CheckCommentIdExists(int commentId);
}