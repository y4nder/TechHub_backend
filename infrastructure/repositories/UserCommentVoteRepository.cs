using domain.entities;
using domain.interfaces;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repositories;

public class UserCommentVoteRepository : IUserCommentVoteRepository
{
    private readonly AppDbContext _context;

    public UserCommentVoteRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddUserCommentVote(UserCommentVote userCommentVote)
    {
        _context.UserCommentVotes.Add(userCommentVote);
    }

    public async Task<UserCommentVote?> GetUserCommentRecord(UserCommentVote userCommentVote)
    {
        return await _context.UserCommentVotes.FirstOrDefaultAsync(c => 
            c.UserId == userCommentVote.UserId &&
            c.CommentId == userCommentVote.CommentId);
    }

    public async Task RemoveUserCommentVote(UserCommentVote userCommentVote)
    {
        var record = await _context.UserCommentVotes
            .AsNoTracking()
            .Where(cv => cv.CommentId == userCommentVote.CommentId && cv.UserId == userCommentVote.UserId)
            .FirstOrDefaultAsync();

        if (record == null)
        {
            throw new KeyNotFoundException("UserCommentVote not found");
        }
        
        _context.UserCommentVotes.Remove(record);
    }
}