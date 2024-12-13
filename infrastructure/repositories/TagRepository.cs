using domain.entities;
using domain.interfaces;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repositories;

public class TagRepository : ITagRepository
{
    private readonly AppDbContext _context;

    public TagRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AreAllIdsValidAsync(List<int> tagIds)
    {
        var validTagIds = await _context.Tags
            .AsNoTracking()
            .Where(e => tagIds.Contains(e.TagId))
            .Select(e => e.TagId)
            .ToListAsync();
        
        return validTagIds.Count == tagIds.Count;
    }

    public async Task<bool> TagIdExistsAsync(int tagId)
    {
        return await _context.Tags
            .AsNoTracking()
            .AnyAsync(e => e.TagId == tagId);
    }

    public async Task<List<Tag>> GetTagsManyAsync(List<int> tagIds)
    {
        return await _context.Tags
            .Where(e => tagIds.Contains(e.TagId))
            .ToListAsync();
    }

    public void BatchTagUpdate(ICollection<Tag> tags)
    {
        foreach (var tag in tags)
        {
            _context.Entry(tag).State = EntityState.Modified;
        }
    }

    public async Task<List<TagDto>> GetTagsByQueryAsync(string tagSearchQuery)
    {
        var query = _context.Tags.AsQueryable();

        var tagQuery = query
            .AsNoTracking()
            .Where(tag =>
                EF.Functions.Like(tag.TagName, $"{tagSearchQuery}%"))
            .OrderByDescending(tag => tag.TagCount)
            .Take(10)
            .Select(tag => new TagDto
            {
                TagId = tag.TagId,
                TagName = tag.TagName
            });

        var tags = await tagQuery.ToListAsync();

        return tags;
    }

    public async Task<bool> AreNewTagsUniqueAsync(List<string> newTags)
    {
        var existingTags = await _context.Tags
            .Where(t => newTags.Contains(t.TagName))
            .Select(t => t.TagName)
            .ToListAsync();
        
        return !existingTags.Any();
    }

    public void BatchAddTags(List<Tag> tags)
    {
        _context.Tags.AddRange(tags);
    }

    public async Task<List<TagDto>> GetAllTagsAsync()
    {
        return await _context.Tags
            .AsNoTracking()
            .Select(tag => new TagDto
            {
                TagId = tag.TagId,
                TagName = tag.TagName
            }).ToListAsync();
    }

    public async Task<List<Tag>> GetArticleTagsAsync(int articleId)
    {
        var article = await _context.Articles.Where(a => a.ArticleId == articleId)
            .Include(a => a.Tags).FirstOrDefaultAsync();
        
        if(article == null)
            throw new Exception($"Article with id {articleId} not found");
        
        return article.Tags.ToList();
    }

    public async Task RemoveTagsAsync(int articleId)
    {
        var rowsDeleted = await _context.Database.ExecuteSqlRawAsync("DELETE FROM [ArticleTag] WHERE ArticleId = {0}", articleId);

        if (rowsDeleted == 0)
        {
            throw new InvalidOperationException("No tags were deleted.");
        }
    }

    public async Task<List<TrendingTagDto>> GetTrendingTagsAsync()
    {
        return await _context.Tags
            .OrderByDescending(tag => tag.TagCount)
            .Take(10)
            .Select(t => new TrendingTagDto
            {
                TagId = t.TagId,
                TagName = t.TagName,
                TagCount = t.TagCount
            }).ToListAsync();
    }

    public async Task<List<GroupedTagList>> GetGroupedTagsAsync()
    {
        var groupedTags = await _context.Tags
            .OrderBy(tag => tag.TagName)
            .GroupBy(tag => tag.TagName.Substring(0, 1).ToUpper()) // Group by the first letter
            .Select(group => new GroupedTagList
            {
                Group = group.Key,
                Tags = group.Select(tag => new TagDto
                {
                    TagId = tag.TagId,
                    TagName = tag.TagName
                }).ToList()
            })
            .ToListAsync();

        return groupedTags;
    }

    public async Task<TagPageDto?> GetTagPageAsync(int tagId, int userId)
    {
        return await _context.Tags
            .Where(t => tagId == t.TagId)
            .Select(t => new TagPageDto
            {
                TagId = t.TagId,
                TagName = t.TagName,
                TagDescription = t.TagDescription,
                Followed = _context.UserTagFollows
                    .Any(ft => ft.TagId == t.TagId && ft.UserId == userId)
            })
            .FirstOrDefaultAsync();
    }

    public async Task<List<TagDto>> GetSuggestedTagsAsync(string searchQuery)
    {
        if (string.IsNullOrWhiteSpace(searchQuery))
            return new();
        
        searchQuery = searchQuery.ToLower(); // Ensure case-insensitivity

        return await _context.Tags
            .AsNoTracking()
            .Where(tag => EF.Functions.Like(tag.TagName.ToLower(), $"{searchQuery}%"))
            .Take(5)
            .Select(t => new TagDto
            {
                TagId = t.TagId,
                TagName = t.TagName
            })
            .ToListAsync();
    }
}