using domain.entities;
using domain.pagination;

namespace domain.interfaces;

public interface IUserFollowRepository
{
    void AddUserFollowRecord(UserFollow userFollow);
    
    Task<bool> CheckUserFollowRecord(int followerId, int followingId);
    
    void RemoveUserFollowRecord(UserFollow userFollow);
    
    Task<UserFollow?> GetUserFollow(int followerId, int followingId);

    Task<UserFollowInfoDto> GetUserFollowInfo(int userId);

    Task<PaginatedResult<UserFollowsListDto>> GetPaginatedFollowersByIdAsync(int userId, int PageNumber, int PageSize);
    Task<PaginatedResult<UserFollowsListDto>> GetPaginatedFollowingByIdAsync(int userId, int pageNumber, int pageSize);
    
    Task<List<UserFollowsListDto>> GetRecommendedUserFollowersByIdAsync(int userId);
}