using CloudinaryDotNet.Actions;
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

        foreach (var club in clubs)
        {
            club.Roles = await _context.ClubUsers
                .AsNoTracking()
                .Where(c => c.ClubId == club.ClubId && c.UserId == userId)
                .Include(c => c.Role)
                .Select(r => new ClubUserRoleMinimalDto
                {
                    RoleId = r.RoleId,
                    RoleName = r.Role!.RoleName!
                }).ToListAsync();
        }
        
        return clubs;
    }

    public Task<List<ClubMinimalDto>?> GetJoinedClubsByIdAsyncVer2(int userId)
    {
        throw new NotImplementedException();
    }


    public async Task<List<ClubUser>?> GetClubUserRecordWithTracking(int clubId, int userId)
    {
        return await _context.ClubUsers
            .Where(cu => cu.ClubId == clubId && cu.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<string>> GetClubNamesByUserId(int userId)
    {
        return await _context.ClubUsers
            .Where(cu => cu.UserId == userId && cu.RoleId == (int)DefaultRoles.RegularUser)
            .Select(cu => cu.Club.ClubName!)
            .ToListAsync();
    }

    public void RemoveClubUserRange(List<ClubUser> clubUsers) => _context.RemoveRange(clubUsers);
    public async Task<ClubUser?> TryRetrieveModeratorRole(int moderatorId)
    {
        return await _context.ClubUsers
            .Where(cu => cu.UserId == moderatorId && cu.RoleId == (int)DefaultRoles.Moderator)
            .FirstOrDefaultAsync();
    }

    public async Task<List<ClubUserRoleDto>> GetModerators(int clubId)
    {
        return await _context.ClubUsers
            .Where(cu => cu.ClubId == clubId &&
                         cu.RoleId == (int)DefaultRoles.Moderator)
            .Include(c => c.Role)
            .Select(cu => new ClubUserRoleDto
            {
                RoleName = cu.Role!.RoleName!,
                Username = cu.User.Username!,
                UserId = cu.UserId,
                UserProfilePicUrl = cu.User.UserProfilePicUrl
            }).ToListAsync();
    }

    public async Task<List<UserDetailsDto>> GetModeratorsFull(int clubId)
    {
         return await _context.ClubUsers
            .AsNoTracking()
            .Where(cu => cu.ClubId == clubId &&
                         cu.RoleId == (int)DefaultRoles.Moderator)
            .Include(c => c.User)
            .ThenInclude(u => u.UserAdditionalInfo)
            .Select(u => new UserDetailsDto
            {
                UserProfilePicUrl = u.User.UserProfilePicUrl,
                Username =  u.User.Username!,
                ReputationPoints = u.User.UserAdditionalInfo!.ReputationPoints,
                UserAdditionalInfo = _context.UserAdditionalInfos
                    .Where(ua => ua.UserId == u.UserId)
                    .Select(ua => new UserAdditionalInfoDto
                    {
                        Bio = ua.Bio,
                        Company = ua.Company,
                        ContactNumber = ua.ContactNumber,
                        Job = ua.Job,
                        GithubLink = ua.GithubLink,
                        LinkedInLink = ua.LinkedInLink,
                        FacebookLink = ua.FacebookLink,
                        XLink = ua.XLink,
                        PersonalWebsiteLink = ua.PersonalWebsiteLink,
                        YoutubeLink = ua.YoutubeLink,
                        StackOverflowLink = ua.StackOverflowLink,
                        RedditLink = ua.RedditLink,
                        ThreadsLink = ua.ThreadsLink,
                    })
                    .First()
            })
            .ToListAsync();
    }

    public async Task<List<int>> GetMemberIds(int postedArticleClubId)
    {
        return await _context.ClubUsers
            .AsNoTracking()
            .Where(cu => cu.ClubId == postedArticleClubId && cu.RoleId == (int)DefaultRoles.RegularUser)
            .Select(cu => cu.UserId)
            .ToListAsync();
    }
}