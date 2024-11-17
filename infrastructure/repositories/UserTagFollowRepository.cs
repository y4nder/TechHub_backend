using domain.entities;
using domain.interfaces;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repositories;

public class UserTagFollowRepository : IUserTagFollowRepository
{
    private readonly AppDbContext _context;

    public UserTagFollowRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddRangeUserTagFollow(List<UserTagFollow> userTagFollows)
    {
        _context.UserTagFollows.AddRange(userTagFollows);
    }

    public async Task<bool> CheckUserFollow(int requestUserId, int requestTagId)
    {
        return await _context.UserTagFollows.AsNoTracking().AnyAsync(u=>u.UserId == requestUserId && u.TagId == requestTagId);
    }

    public async Task<UserTagFollow?> GetUserTagFollow(int userId, int tagId)
    {
        return await _context.UserTagFollows.FirstOrDefaultAsync(u => u.UserId == userId && u.TagId == tagId);
    }

    public void AddUserFollow(UserTagFollow userTagFollow)
    {
        _context.UserTagFollows.Add(userTagFollow);
    }

    public void RemoveUserFollow(UserTagFollow userTagFollow)
    {
        _context.UserTagFollows.Remove(userTagFollow);
    }

    public async Task<List<UserTagFollow>?> GetFollowedTags(int userId)
    {
        return await _context.UserTagFollows
            .AsNoTracking()
            .Where(u => u.UserId == userId)
            .ToListAsync();
    }
}