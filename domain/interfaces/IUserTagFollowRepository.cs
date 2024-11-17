using domain.entities;

namespace domain.interfaces;

public interface IUserTagFollowRepository
{
    public void AddRangeUserTagFollow(List<UserTagFollow> userTagFollows);
    public Task<bool> CheckUserFollow(int userId, int tagId);
    
    public Task<UserTagFollow?> GetUserTagFollow(int userId, int tagId);
    
    public void AddUserFollow(UserTagFollow userTagFollow);
    
    public void RemoveUserFollow(UserTagFollow userTagFollow);
    Task<List<UserTagFollow>?> GetFollowedTags(int userId);
}