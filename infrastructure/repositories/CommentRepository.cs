using domain.entities;
using domain.interfaces;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repositories;

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _context;

    public CommentRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddComment(Comment comment)
    {
        _context.Comments.Add(comment);
    }

    public async Task<bool> ParentCommentIdExists(int parentCommentId)
    {
        return await _context.Comments
            .AsNoTracking()
            .AnyAsync(c => c.CommentId == parentCommentId);
    }
}