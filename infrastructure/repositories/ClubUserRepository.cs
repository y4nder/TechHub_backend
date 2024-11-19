using domain.entities;
using domain.interfaces;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repositories;

public class ClubUserRepository : IClubUserRepository
{
    private readonly AppDbContext _context;

    public ClubUserRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddClubUser(ClubUser clubUser)
    {
        _context.ClubUsers.Add(clubUser);
    }

    public async Task AddClubUserRange(List<ClubUser> clubUsers)
    {
        foreach (var clubUser in clubUsers)
        {
            var exists = await _context.ClubUsers.AnyAsync(cu =>
                cu.ClubId == clubUser.ClubId &&
                cu.RoleId == clubUser.RoleId &&
                cu.UserId == clubUser.UserId);

            if (!exists)
            {
                await _context.AddAsync(clubUser);
            }
        }
    }

    public async Task<bool> ClubJoined(int clubId, int userId)
    {
        return await _context.ClubUsers.AsNoTracking().AnyAsync(c => c.ClubId == clubId && c.UserId == userId);
    }

    public async Task<List<ClubUser>?> GetClubUserRecord(int clubId, int userId)
    {
        return await _context.ClubUsers
            .AsNoTracking()
            .Where(c => c.UserId == userId && c.ClubId == clubId)
            .Include(c => c.Role)
            .ToListAsync();
    }
}