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
}