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
}