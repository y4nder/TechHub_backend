using domain.entities;
using domain.interfaces;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repositories;

public class UserArticleVoteRepository : IUserArticleVoteRepository
{
    private readonly AppDbContext _context;

    public UserArticleVoteRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddUserArticleVote(UserArticleVote userArticleVote)
    {
        _context.UserArticleVotes.Add(userArticleVote);
    }

    public async Task<bool> CheckAlreadyVoted(UserArticleVote userArticleVote)
    {
        return await _context.UserArticleVotes
            .AsNoTracking()
            .AnyAsync(v => 
                v.UserId == userArticleVote.UserId && 
                v.ArticleId == userArticleVote.ArticleId);
    }

    public async Task<UserArticleVote?> GetUserArticleVoteByRecord(UserArticleVote userArticleVote)
    {
        return await _context.UserArticleVotes
            .FirstOrDefaultAsync(v =>
                v.UserId == userArticleVote.UserId &&
                v.ArticleId == userArticleVote.ArticleId);
    }

    public async Task<int> GetArticleVoteCount(int articleId)
    {
        return await _context.UserArticleVotes
            .Where(a => a.ArticleId == articleId)
            .SumAsync(v => v.VoteType);
    }

    public async Task RemoveUserArticleVote(UserArticleVote userArticleVote)
    {
        var record = await _context.UserArticleVotes
            .AsNoTracking()
            .Where(v => v.ArticleId == userArticleVote.ArticleId && v.UserId == userArticleVote.UserId)
            .FirstOrDefaultAsync();
        
        if(record == null)
            throw new Exception("UserArticleVote not found");
                     
        _context.UserArticleVotes.Remove(userArticleVote);
    }

    public Task<short> GetArticleVoteType(int articleId, int userId)
    {
        var voteType = _context.UserArticleVotes
            .AsNoTracking()
            .Where(v => v.ArticleId == articleId && v.UserId == userId)
            .Select(v => v.VoteType).FirstOrDefaultAsync();
        
        return voteType;
    }
}