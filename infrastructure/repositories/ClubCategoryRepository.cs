using domain.interfaces;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repositories;

public class ClubCategoryRepository : IClubCategoryRepository
{
    private readonly AppDbContext _context;

    public ClubCategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CheckClubCategoryExists(int clubCategoryId)
    {
        return await _context.ClubCategories
            .AsNoTracking()
            .AnyAsync(c => c.ClubCategoryId == clubCategoryId);
    }
}