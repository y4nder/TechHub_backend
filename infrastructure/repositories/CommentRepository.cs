using domain.entities;
using domain.interfaces;
using domain.pagination;
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

    public async Task<PaginatedResult<ArticleCommentDto>> GetPaginatedCommentsByArticleId(
        int articleId, int pageNumber, int pageSize)
    {
        // Base query with filtering and ordering
        var query = _context.Comments
            .AsNoTracking()
            .Include(c => c.CommentCreator.UserAdditionalInfo) // Eager load only needed relationships
            .Include(c => c.InverseParentComment)
                .ThenInclude(r => r.CommentCreator.UserAdditionalInfo)
            .Where(c => c.ArticleId == articleId && c.ParentCommentId == null)
            .OrderBy(c => c.CreatedDateTime);
        
        // Get total count of comments for pagination
        var totalCount = await query.CountAsync();
        
        // Paginated comments with VoteCount computation and projection to DTO
        var comments = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(c => new ArticleCommentDto(c, 
                _context.UserCommentVotes
                            .Where(v => v.CommentId == c.CommentId)
                            .Sum(v => v.VoteType)))
            .ToListAsync();
        
        // Return paginated result
        return new PaginatedResult<ArticleCommentDto>
        {
            Items = comments,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }

    public async Task<int> GetTotalCommentsByArticleId(int articleId)
    {
        return await _context.Comments.Where(c => c.ArticleId == articleId).CountAsync();
    }

    public async Task<bool> CheckCommentIdExists(int commentId)
    {
        return await _context.Comments.FindAsync(commentId) != null;
    }
}