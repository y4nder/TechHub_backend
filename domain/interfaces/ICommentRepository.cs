using domain.entities;

namespace domain.interfaces;

public interface ICommentRepository
{
    void AddComment(Comment comment);
    
    Task<bool> ParentCommentIdExists(int parentCommentId);
}