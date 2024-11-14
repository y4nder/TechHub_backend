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
            .Where(e => tagIds.Contains(e.TagId))
            .Select(e => e.TagId)
            .ToListAsync();
        
        return validTagIds.Count == tagIds.Count;
    }

    public async Task<bool> TagIdExistsAsync(int tagId)
    {
        return await _context.Tags.AsNoTracking().AnyAsync(e => e.TagId == tagId);
    }
}