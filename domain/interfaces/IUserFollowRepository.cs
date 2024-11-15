using domain.entities;

namespace domain.interfaces;

public interface IUserFollowRepository
{
    void AddUserFollowRecord(UserFollow userFollow);
    
    Task<bool> CheckUserFollowRecord(int followerId, int followingId);
    
    void RemoveUserFollowRecord(UserFollow userFollow);
    
    Task<UserFollow?> GetUserFollow(int followerId, int followingId);
}