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

    public async Task<Club?> GetClubByIdNoTracking(int clubId)
    {
        return await _context.Clubs
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.ClubId == clubId);
    }

    public async Task<List<ClubCategoryStandardResponseDto>> GetAllCategorizedClubs()
    {
        //fetch categories
        var categories = await _context.ClubCategories
            .AsNoTracking()
            .Select(cat => new ClubCategoryDto
            {
                ClubCategoryId = cat.ClubCategoryId,
                ClubCategoryName = cat.ClubCategoryName
            })
            .ToListAsync();

        var result = new List<ClubCategoryStandardResponseDto>();
        
        foreach (var category in categories)
        {
            var clubCategoryDto = new ClubCategoryStandardResponseDto
            {
                CategoryId = category.ClubCategoryId,
                CategoryName = category.ClubCategoryName,
                Clubs = _context.Clubs
                    .AsNoTracking()
                    .Where(c => c.ClubCategoryId == category.ClubCategoryId && c.Private != true)
                    .Select(club => new ClubStandardResponseDto
                    {
                        ClubId = club.ClubId,
                        ClubProfilePicUrl = club.ClubImageUrl!,
                        ClubName = club.ClubName!,
                        ClubDescription = club.ClubIntroduction!,
                        ClubMembersCount = club.ClubUsers
                            .Select(cu => new {cu.ClubId, cu.UserId})
                            .Distinct().Count()
                    }).ToList()
            };
            
            result.Add(clubCategoryDto);
        }
        
        return result;
    }

    public async Task<List<ClubFeaturedResponseDto>> GetFeaturedClubsAsync()
    {
        var query = _context.Clubs
            .AsNoTracking()
            .OrderByDescending(club => club.ClubViews)
            .ThenByDescending(club
                => club.ClubUsers
                    .Select(cu => new { cu.ClubId, cu.UserId })
                    .Distinct().Count())
            .Where(club => club.Featured == true)
            .Select(club => new ClubFeaturedResponseDto
            {
                ClubId = club.ClubId,
                ClubProfilePicUrl = club.ClubImageUrl!,
                ClubName = club.ClubName!,
                ClubDescription = club.ClubIntroduction!,
                ClubMembersCount = club.ClubUsers
                    .Select(cu => new {cu.ClubId, cu.UserId})
                    .Distinct().Count(),
                RecentMembersProfilePics = club
                    .ClubUsers
                    .Select( cu => new RecentMembersProfileResponseDto
                    {
                        UserId = cu.UserId,
                        UserProfilePicUrl = cu.User.UserProfilePicUrl,
                        Username = cu.User.Username!
                    })
                    .Take(10)
                    .Distinct()
                    .ToList()
            });
        
        var result = await query.ToListAsync();

        return result;
    }

    public async Task<SingleClubResponseDto?> GetSingleClubByIdAsync(int userId, int clubId)
    {
        var query = _context.Clubs
            .AsNoTracking()
            .Where(club => club.ClubId == clubId)
            .AsSplitQuery() // Enable split query for this specific query
            .Select(club =>
                new SingleClubResponseDto
                {
                    ClubId = club.ClubId,
                    ClubName = club.ClubName!, 
                    ClubProfilePicUrl = club.ClubImageUrl!,
                    PostCount = club.Articles.Count,
                    ClubViews = club.ClubViews,
                    ClubCreatedDateTime = club.ClubAdditionalInfo!.ClubCreatedDate,
                    ClubUpVoteCount = club.Articles
                        .SelectMany(a => a.UserArticleVotes)
                        .Sum(v => v.VoteType),
                    Featured = club.Featured,
                    Private = club.Private,
                    RecentMemberProfilePics = club.ClubUsers
                        .Select( cu => new RecentMembersProfileResponseDto
                        {
                            UserId = cu.UserId,
                            UserProfilePicUrl = cu.User.UserProfilePicUrl,
                            Username = cu.User.Username!
                        })
                        .Distinct()
                        .Take(5)
                        .ToList(),
                    ClubIntroduction = club.ClubIntroduction!,
                    MemberCount =  club.ClubUsers
                        .GroupBy(cu => new {cu.ClubId, cu.UserId})
                        .Count(),
                    ClubCreator =  new ClubUserRoleDto
                    {
                        UserId = club.ClubCreator!.UserId,
                        Username = club.ClubCreator.Username!,
                        RoleName = DefaultRoles.ClubCreator.ToString(),
                        UserProfilePicUrl = club.ClubCreator.UserProfilePicUrl
                    },
                    Moderators = club.ClubUsers
                        .Where(cu => 
                            cu.Role != null &&
                            cu.RoleId != (int)DefaultRoles.RegularUser &&
                            cu.UserId != club.ClubCreator.UserId)
                        .Distinct()
                        .Select(cu => new ClubUserRoleDto
                        {
                            UserId = cu.UserId,
                            Username = cu.User.Username!,
                            RoleName = cu.Role!.RoleName!,
                            UserProfilePicUrl = cu.User.UserProfilePicUrl,
                        })
                        .ToList(),
                    Joined = club.ClubUsers
                        .Any(cu => cu.UserId == userId)
                });
        
        var result = await query.FirstOrDefaultAsync();
        
        return result;
    }

    public async Task<Club?> GetClubByIdNo(int clubId)
    {
        return await _context.Clubs.FindAsync(clubId);
    }

    public async Task<ClubInfoDto?> GetClubInfoByIdNoTracking(int clubId)
    {
        return await _context.Clubs.AsNoTracking()
            .Where(c => c.ClubId == clubId)
            .Select(c => new ClubInfoDto
            {
                ClubId = c.ClubId,
                ClubName = c.ClubName!,
                ClubProfilePicUrl = c.ClubImageUrl!,
                ClubIntroduction = c.ClubIntroduction!,
            }).FirstOrDefaultAsync();
    }

    public void UpdateClub(Club club)
    {
        _context.Clubs.Update(club);
    }

    public Task<List<ClubMinimalDto>?> GetJoinedClubsByIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<SingleCategoryClubStandardResponseDto?> GetSingleCategoryClubByIdAsync(int clubCategoryId)
    {
        var categoryClub =  await _context.Clubs
            .AsNoTracking()
            .Where(c => c.ClubCategoryId == clubCategoryId && c.Private != true)
            .Select(club => new SingleCategoryClubStandardResponseDto
            {
                CategoryId = club.ClubCategoryId,
                CategoryName = club.ClubCategory!.ClubCategoryName,
                Clubs = _context.Clubs
                    .AsNoTracking()
                    .Where(c => c.ClubCategoryId == club.ClubCategoryId)
                    .Select(cl => new ClubStandardResponseDto
                    {
                        ClubId = cl.ClubId,
                        ClubProfilePicUrl = cl.ClubImageUrl!,
                        ClubName = cl.ClubName!,
                        ClubDescription = cl.ClubIntroduction!,
                        ClubMembersCount = cl.ClubUsers
                            .Select(cu => new { cu.ClubId, cu.UserId })
                            .Distinct().Count()
                    }).ToList() ?? new()
            }).FirstOrDefaultAsync();

        if (categoryClub == null)
        {
            return new SingleCategoryClubStandardResponseDto
            {
                CategoryId = -1,
                CategoryName = "",
                Clubs = new List<ClubStandardResponseDto>()
            };
        }
        else
        {
            return categoryClub;
        } 
            
    }

    public async Task<List<SuggestedClubDto>> GetSuggestedClubs(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return new ();
        
        searchTerm = searchTerm.ToUpper();

        return await _context.Clubs
            .AsNoTracking()
            .Where(club => EF.Functions.Like(club.ClubName!.ToUpper(), $"{searchTerm}%") && !club.Private)
            .Take(3)
            .Select(t => new SuggestedClubDto
            {
                ClubId = t.ClubId,
                ClubName = t.ClubName!,
                ClubProfilePicUrl = t.ClubImageUrl!
            })
            .ToListAsync();
    }

    public async Task<List<ClubUserMinimalDto>> GetClubUsers(int clubId, string searchTerm)
    {
        return await _context.ClubUsers
            .AsNoTracking()
            .Include(c => c.User)
            .ThenInclude(u => u.UserAdditionalInfo)
            .Include(c => c.Role)
            .Where(c => 
                c.ClubId == clubId && 
                c.RoleId == (int)DefaultRoles.RegularUser &&
                (string.IsNullOrEmpty(searchTerm) || EF.Functions.Like(c.User.Username!.ToUpper(), $"{searchTerm}%")))
            .Select(cu => new ClubUserMinimalDto
            {
                UserId = cu.UserId,
                Username = cu.User.Username!,
                UserProfilePicUrl = cu.User.UserProfilePicUrl,
                Email = cu.User.Email!,
                ReputationPoints = cu.User.UserAdditionalInfo!.ReputationPoints,
                DateJoined = cu.DateJoined,
                Roles = _context.ClubUsers.Where(c => c.ClubId == clubId && c.UserId == cu.UserId)
                    .Select(cur => new ClubUserRoleMinimalDto
                    {
                        RoleId = cur.RoleId,
                        RoleName = cur.Role!.RoleName!
                    }).ToList()
            })
            .ToListAsync();
    }

    public async Task<int> GetClubUsersCount(int clubId)
    {
        var baseQuery = _context.ClubUsers
            .AsNoTracking()
            .Where(c => 
                c.ClubId == clubId && 
                c.RoleId != (int)DefaultRoles.ClubCreator &&
                c.RoleId != (int)DefaultRoles.Moderator) ;
        
        var count = await baseQuery.CountAsync();
            
        return count;
    }

    public async Task UpdateUserRolesTransaction(List<ClubUser> userRoles, int clubId, int userId)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Fetch existing records
            var existingRecords = await _context.ClubUsers
                .Where(cu => cu.ClubId == clubId && cu.UserId == userId && cu.RoleId != (int)DefaultRoles.ClubCreator)
                .ToListAsync();

            // Find records to remove
            var recordsToRemove = existingRecords
                .Where(er => !userRoles.Any(ur => ur.RoleId == er.RoleId))
                .ToList();

            // Find records to add
            var recordsToAdd = userRoles
                .Where(ur => !existingRecords.Any(er => er.RoleId == ur.RoleId))
                .ToList();

            // Apply changes
            _context.ClubUsers.RemoveRange(recordsToRemove);
            _context.ClubUsers.AddRange(recordsToAdd);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<ClubForEditDto?> GetClubForEdit(int clubId)
    {
        return await _context.Clubs
            .AsNoTracking()
            .Where(c => c.ClubId == clubId)
            .Select(c => new ClubForEditDto
            {
                ClubId = c.ClubId,
                ClubCategoryId = c.ClubCategoryId,
                ClubIntroduction = c.ClubIntroduction,
                ClubName = c.ClubName!,
                ClubThumbnailUrl = c.ClubImageUrl!,
                InvitePermission = c.InvitePermission,
                PostPermission = c.PostPermission,
                IsPrivate = c.Private,
            }).FirstOrDefaultAsync();
    }

    public async Task<ClubInfoDto?> GetArticleClub(int clubId)
    {
        return await _context.Clubs.AsNoTracking()
            .Where(c => c.ClubId == clubId)
            .Select(c => new ClubInfoDto
            {
                ClubId = c.ClubId,
                ClubName = c.ClubName!,
                ClubProfilePicUrl = c.ClubImageUrl!,
                ClubIntroduction = c.ClubIntroduction!,
            }).FirstOrDefaultAsync();
    }
}