using domain.entities;
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

    public async Task<List<ClubCategoryDto>> GetAllClubCategoriesAsync()
    {
        return await _context.ClubCategories
            .AsNoTracking()
            .Select(c => new ClubCategoryDto
            {
                ClubCategoryId = c.ClubCategoryId,
                ClubCategoryName = c.ClubCategoryName
            })
            .ToListAsync();
    }
}