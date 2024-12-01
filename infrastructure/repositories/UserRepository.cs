using domain.entities;
using domain.interfaces;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CheckUserNameExists(string username)
    {
        return await _context.Users
            .AsNoTracking()
            .AnyAsync(u => u.Username == username);
    }

    public async Task<bool> CheckUserEmailExists(string emailAddress)
    {
        return await _context.Users
            .AsNoTracking()
            .AnyAsync(u => u.Email == emailAddress);
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
    }

    public async Task<User?> GetUserByEmail(string emailAddress)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == emailAddress);
    }

    public async Task<bool> CheckIdExists(int userId)
    {
        return await _context.Users.AsNoTracking().AnyAsync(u => u.UserId == userId);
    }

    public async Task<User?> GetUserById(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<User?> GetUserWithRolesByIdNoTracking(int userId)
    {
        return await _context.Users
            .AsNoTracking()
            .Include(u => u.ClubUsers)
            .FirstOrDefaultAsync(u => u.UserId == userId);
    }

    public async Task<UserMinimalDto?> GetMinimalUserByIdAsync(int requestUserId)
    {
        return await _context.Users
            .AsNoTracking()
            .Include(u => u.UserAdditionalInfo) 
            .Where(u => u.UserId == requestUserId) 
            .Select(u => new UserMinimalDto(u))
            .FirstOrDefaultAsync();
    }

    public async Task<UserProfileDto?> GetUserProfileByIdAsync(int userId)
    {
        return await _context.Users
            .AsNoTracking()
            .Where(u => u.UserId == userId)
            .Select(u => new UserProfileDto
            {
                Username = u.Username!,
                UserProfilePicUrl = u.UserProfilePicUrl
            }).FirstOrDefaultAsync();
    }
}