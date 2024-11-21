using domain.entities;
using domain.interfaces;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repositories;

public class ArticleBookmarkRepository : IArticleBookmarkRepository
{
    private readonly AppDbContext _context;

    public ArticleBookmarkRepository(AppDbContext context)
    {
        _context = context;
    }


    public void AddBookmark(UserArticleBookmark bookmark)
    {
        _context.UserArticleBookmarks.Add(bookmark);
    }

    public async Task<bool> BookmarkExist(UserArticleBookmark bookmark)
    {
        return await _context.UserArticleBookmarks
            .AsNoTracking()
            .AnyAsync(b => 
                b.UserId == bookmark.UserId && 
                b.ArticleId == bookmark.ArticleId);
    }
}