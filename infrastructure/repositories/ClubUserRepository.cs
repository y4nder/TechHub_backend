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

    public async Task<List<ClubUser>?> GetClubUserRecords(int clubId, int userId)
    {
        return await _context.ClubUsers
            .AsNoTracking()
            .Where(c => c.UserId == userId && c.ClubId == clubId)
            .Include(c => c.Role)
            .ToListAsync();
    }

    public async Task<List<ClubMinimalDto>?> GetJoinedClubsByIdAsync(int userId)
    {
        var clubs = await _context.ClubUsers
            .FromSqlInterpolated($@"
                SELECT DISTINCT 
                    cu.userId, cu.clubId, c.clubImageUrl, c.clubName
                FROM ClubUser cu
                JOIN Club c on c.clubId = cu.clubId
                WHERE cu.userId = {userId}
            ")
            .Select(cu => new ClubMinimalDto
            {
                ClubId = cu.ClubId,
                ClubProfilePicUrl = cu.Club.ClubImageUrl!,
                ClubName = cu.Club.ClubName!
            }).ToListAsync();
        
        return clubs;
    }

    public async Task<List<ClubUser>?> GetClubUserRecordWithTracking(int clubId, int userId)
    {
        return await _context.ClubUsers
            .Where(cu => cu.ClubId == clubId && cu.UserId == userId)
            .ToListAsync();
    }

    public void RemoveClubUserRange(List<ClubUser> clubUsers) => _context.RemoveRange(clubUsers);
}