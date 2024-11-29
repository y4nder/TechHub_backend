using domain.entities;

namespace domain.interfaces;

public interface IUserRepository
{
    Task<bool> CheckUserNameExists(string username);
    Task<bool> CheckUserEmailExists(string emailAddress);
    
    void AddUser(User user);

    Task<User?> GetUserByEmail(string emailAddress);
    
    Task<bool> CheckIdExists(int userId);
    
    Task<User?> GetUserById(int userId);
    Task<User?> GetUserWithRolesByIdNoTracking(int userId);


    Task<UserMinimalDto?> GetMinimalUserByIdAsync(int requestUserId);
}