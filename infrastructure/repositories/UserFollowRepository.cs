using domain.entities;
using domain.interfaces;
using domain.pagination;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repositories;

public class UserFollowRepository : IUserFollowRepository
{
    private readonly AppDbContext _context;

    public UserFollowRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddUserFollowRecord(UserFollow userFollow)
    {
        _context.UserFollows.Add(userFollow);
    }

    public async Task<bool> CheckUserFollowRecord(int followerId, int followingId)
    {
        return await _context.UserFollows.AsNoTracking().AnyAsync(u => u.FollowerId == followerId && u.FollowingId == followingId); 
    }

    public void RemoveUserFollowRecord(UserFollow userFollow)
    {
        _context.UserFollows.Remove(userFollow);
    }

    public async Task<UserFollow?> GetUserFollow(int followerId, int followingId)
    {
        return await _context.UserFollows.FirstOrDefaultAsync(u => u.FollowerId == followerId && u.FollowingId == followingId); 
    }

    public async Task<UserFollowInfoDto> GetUserFollowInfo(int userId)
    {
        var followingCount = await _context.UserFollows
            .CountAsync(f => f.FollowerId == userId);
        
        var followerCount = await _context.UserFollows
            .CountAsync(f => f.FollowingId == userId);

        return new UserFollowInfoDto
        {
            FollowerCount = followerCount,
            FollowingCount = followingCount
        };
    }

    public async Task<PaginatedResult<UserFollowsListDto>> GetPaginatedFollowersByIdAsync(int userId, int pageNumber, int pageSize)
    {
        var baseQuery = _context.UserFollows
            .Where(uf => uf.FollowingId == userId);

        return await GetPaginatedFollowersByIdAsync(baseQuery, userId, pageNumber, pageSize);
    }

    private async Task<PaginatedResult<UserFollowsListDto>> GetPaginatedFollowersByIdAsync(IQueryable<UserFollow> baseQuery,int userId, int pageNumber, int pageSize)
    {
        var totalCount = await baseQuery.CountAsync();

        var followers = await baseQuery
            .AsNoTracking()
            .OrderByDescending(f => f.FollowedDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(f => new UserFollowsListDto
            {
                UserId = f.FollowerId,
                Username = f.Follower.Username!,
                UserProfilePicUrl = f.Follower.UserProfilePicUrl,
                Email = f.Follower.Email!,
                ReputationPoints = f.Follower.UserAdditionalInfo!.ReputationPoints,
                Followed = _context.UserFollows.Any(uf => uf.FollowerId == userId && uf.FollowingId == f.FollowerId)
            }).ToListAsync();


        return new PaginatedResult<UserFollowsListDto>
        {
            Items = followers,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        
    }
    public async Task<PaginatedResult<UserFollowsListDto>> GetPaginatedFollowingByIdAsync(int userId, int pageNumber, int pageSize)
    {
        var baseQuery = _context.UserFollows
            .Where(uf => uf.FollowerId == userId);

        return await GetPaginatedFollowingByIdAsync(baseQuery, userId, pageNumber, pageSize);
    }

    public async Task<List<UserFollowsListDto>> GetRecommendedUserFollowersByIdAsync(int userId)
    {
        // Retrieve IDs of users the current user is already following
        var followedUserIds = await _context.UserFollows
            .AsNoTracking()
            .Where(uf => uf.FollowerId == userId)
            .Select(uf => uf.FollowingId)
            .ToListAsync();

        // Retrieve users not followed by the current user
        return await _context.Users
            .AsNoTracking()
            .Where(u => u.UserId != userId && !followedUserIds.Contains(u.UserId)) // Exclude current user and followed users
            .Select(u => new UserFollowsListDto
            {
                UserId = u.UserId,
                Username = u.Username!,
                UserProfilePicUrl = u.UserProfilePicUrl,
                Email = u.Email!,
                ReputationPoints = u.UserAdditionalInfo!.ReputationPoints,
                Followed = false
            })
            .Take(5) // Limit the number of results
            .ToListAsync();
    }


    private async Task<PaginatedResult<UserFollowsListDto>> GetPaginatedFollowingByIdAsync(IQueryable<UserFollow> baseQuery, int userId, int pageNumber, int pageSize)
    {
        var totalCount = await baseQuery.CountAsync();

        var followers = await baseQuery
            .AsNoTracking()
            .OrderByDescending(f => f.FollowedDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(f => new UserFollowsListDto
            {
                UserId = f.FollowingId,
                Username = f.Following.Username!,
                UserProfilePicUrl = f.Following.UserProfilePicUrl,
                Email = f.Following.Email!,
                ReputationPoints = f.Following.UserAdditionalInfo!.ReputationPoints,
                Followed = true
            }).ToListAsync();


        return new PaginatedResult<UserFollowsListDto>
        {
            Items = followers,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

    }

    
}