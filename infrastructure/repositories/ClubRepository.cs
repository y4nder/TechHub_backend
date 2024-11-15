using domain.entities;
using domain.interfaces;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repositories;

public class ClubRepository : IClubRepository
{
    private readonly AppDbContext _context;

    public ClubRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CheckClubNameExists(string clubName)
    {
        return await _context.Clubs.AsNoTracking().AnyAsync(c => c.ClubName == clubName);
    }

    public void AddNewClub(Club club)
    {
        _context.Clubs.Add(club);
    }

    public async Task<bool> ClubIdExists(int clubId)
    {
        return await _context.Clubs.AsNoTracking().AnyAsync(c => c.ClubId == clubId);
    }
}