using domain.entities;
using domain.interfaces;
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
        var followerCount = await _context.UserFollows
            .CountAsync(f => f.FollowerId == userId);
        
        var followingCount = await _context.UserFollows
            .CountAsync(f => f.FollowingId == userId);

        return new UserFollowInfoDto
        {
            FollowerCount = followerCount,
            FollowingCount = followingCount
        };
    }
}