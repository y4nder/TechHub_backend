using domain.entities;
using domain.interfaces;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repositories;

public class UserArticleReadRepository : IUserArticleReadRepository
{
    private readonly AppDbContext _context;

    public UserArticleReadRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddReadHistory(UserArticleRead userArticleRead)
    {
        _context.UserArticleReads.Add(userArticleRead);
    }

    public async Task<List<UserArticleRead>?> GetUserReadHistories(int userId)
    {
        return await _context.UserArticleReads.AsNoTracking()
            .Where(u => u.UserId == userId)
            .ToListAsync();
    }

    public async Task<UserArticleRead?> GetUserArticleRead(int userId, int articleId)
    {
        return await _context.UserArticleReads
            .Where(u => u.ArticleId == articleId && u.UserId == userId)
            .FirstOrDefaultAsync();
    }
}