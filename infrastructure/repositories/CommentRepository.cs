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
    //
    // public async Task<PaginatedResult<ArticleCommentDto>> GetPaginatedCommentsByArticleId(
    //     int articleId, int pageNumber, int pageSize)
    // {
    //     // Base query with filtering and ordering
    //     var query = _context.Comments
    //         .AsNoTracking()
    //         .Include(c => c.CommentCreator.UserAdditionalInfo) // Eager load only needed relationships
    //         .Include(c => c.InverseParentComment)
    //             .ThenInclude(r => r.CommentCreator.UserAdditionalInfo)
    //         .Where(c => c.ArticleId == articleId && c.ParentCommentId == null)
    //         .OrderBy(c => c.CreatedDateTime);
    //     
    //     // Get total count of comments for pagination
    //     var totalCount = await query.CountAsync();
    //     
    //     // Paginated comments with VoteCount computation and projection to DTO
    //     var comments = await query
    //         .Skip((pageNumber - 1) * pageSize)
    //         .Take(pageSize)
    //         .Select(c => new ArticleCommentDto(c, 
    //             _context.UserCommentVotes
    //                         .Where(v => v.CommentId == c.CommentId)
    //                         .Sum(v => v.VoteType),
    //                         
    //             ))
    //         .ToListAsync();
    //     
    //     // Return paginated result
    //     return new PaginatedResult<ArticleCommentDto>
    //     {
    //         Items = comments,
    //         TotalCount = totalCount,
    //         PageNumber = pageNumber,
    //         PageSize = pageSize,
    //         TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
    //     };
    // }
    
    public async Task<PaginatedResult<ArticleCommentDto>> GetPaginatedCommentsByArticleId(int userId, int articleId,
        int pageNumber, int pageSize)
    {
        // Load all comments and replies into memory
        var commentsQuery = _context.Comments
            .AsNoTracking()
            .Include(c => c.CommentCreator.UserAdditionalInfo)
            .Include(c => c.InverseParentComment)
                .ThenInclude(r => r.CommentCreator.UserAdditionalInfo)
            .Where(c => c.ArticleId == articleId)
            .OrderBy(c => c.CreatedDateTime);

        // Get all comments including replies
        var allComments = await commentsQuery.ToListAsync();

        // Separate parent comments and replies
        var parentComments = allComments.Where(c => c.ParentCommentId == null).ToList();
        var allReplies = allComments.Where(c => c.ParentCommentId != null).ToList();

        // Prepare dictionary for comments with their replies
        var commentRepliesDict = parentComments.ToDictionary(
            c => c.CommentId, 
            c => allReplies.Where(reply => reply.ParentCommentId == c.CommentId).ToList()
        );

        // Fetch vote counts for each comment and reply
        var voteSums = await _context.UserCommentVotes
            .Where(v => v.Comment.ArticleId == articleId)
            .GroupBy(v => v.CommentId)
            .Select(g => new { CommentId = g.Key, VoteSum = g.Sum(v => v.VoteType) })
            .ToListAsync();

        // Fetch vote types for each comment for the user
        var voteTypes = await _context.UserCommentVotes
            .Where(v => v.UserId == userId && parentComments.Select(c => c.CommentId).Contains(v.CommentId))
            .ToListAsync();

        // Create the result with vote counts for each comment and reply
        var comments = parentComments.Select(c =>
        {
            var voteType = voteTypes.FirstOrDefault(v => v.CommentId == c.CommentId)?.VoteType ?? 0;

            var replies = commentRepliesDict.ContainsKey(c.CommentId)
                ? commentRepliesDict[c.CommentId].Select(reply =>
                {
                    var replyVoteType = voteTypes.FirstOrDefault(v => v.CommentId == reply.CommentId)?.VoteType ?? 0;
                    return new ArticleCommentDto(
                        reply,
                        voteSums.FirstOrDefault(v => v.CommentId == reply.CommentId)?.VoteSum ?? 0,
                        replyVoteType
                    );
                }).ToList()
                : new List<ArticleCommentDto>();

            return new ArticleCommentDto(
                c,
                voteSums.FirstOrDefault(v => v.CommentId == c.CommentId)?.VoteSum ?? 0,
                voteType,
                replies
            );
        }).ToList();

        // Pagination
        var totalCount = comments.Count;
        var pagedComments = comments.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        // Return paginated result
        return new PaginatedResult<ArticleCommentDto>
        {
            Items = pagedComments,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }


    // Method to get the VoteType (1, 0, -1) for a specific user and comment
    private async Task<int> GetVoteTypeForComment(int userId, int commentId)
    {
        var vote = await _context.UserCommentVotes
            .Where(v => v.UserId == userId && v.CommentId == commentId)
            .FirstOrDefaultAsync();

        if (vote == null)
        {
            return 0; // No vote
        }

        return vote.VoteType; // Return the vote type: 1 (upvote), -1 (downvote), or 0 (no vote)
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