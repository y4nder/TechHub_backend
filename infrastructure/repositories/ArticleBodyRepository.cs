using domain.entities;
using domain.interfaces;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repositories;

public class ArticleBodyRepository : IArticleBodyRepository
{
    private readonly AppDbContext _context;

    public ArticleBodyRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddArticleBody(ArticleBody articleBody)
    {
        _context.ArticleBodies.Add(articleBody);
    }

    public async Task<ArticleBody?> GetArticleBodyByIdAsync(int articleId)
    {
        return await _context.ArticleBodies
            .FindAsync(articleId);
    }

    public async Task<ArticleBodyDto?> GetArticleBodyDtoByIdAsync(int articleId)
    {
        return await _context.ArticleBodies
            .AsNoTracking()
            .Where(a => a.ArticleId == articleId)
            .Select(a => new ArticleBodyDto
            {
                ArticleContent = a.ArticleContent,
                ArticleHtmlContent = a.ArticleHtmlContent
            }).FirstOrDefaultAsync();
        
    }

    public async Task<string?> GetArticleHtmlContentByIdAsync(int articleId)
    {

        return await _context.ArticleBodies
            .AsNoTracking()
            .Where(a => a.ArticleId == articleId)
            .Select(a => a.ArticleHtmlContent)
            .FirstOrDefaultAsync();
    }
}