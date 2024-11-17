using domain.entities;
using domain.interfaces;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly AppDbContext _context;

    public ArticleRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddArticle(Article article)
    {
        _context.Articles.Add(article);
    }

    public async Task<Article?> GetArticleByIdAsync(int articleId)
    {
        return await _context.Articles.FindAsync(articleId);
    }

    public void Update(Article article)
    {
        _context.Entry(article).State = EntityState.Modified;
    }

    public async Task<bool> ArticleExistsAsync(int articleId)
    {
        return await _context.Articles
            .AsNoTracking()
            .AnyAsync(a => a.ArticleId == articleId);
    }

    public async Task<Article?> GetArticleByIdNoTracking(int articleId)
    {
        return await  _context.Articles
            .AsNoTracking()
            .Where(a => a.ArticleId == articleId)
            .FirstOrDefaultAsync(); 
    }
}