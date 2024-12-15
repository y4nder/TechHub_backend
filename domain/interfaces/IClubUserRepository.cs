using domain.entities;

namespace domain.interfaces;

public interface IClubUserRepository
{
    void AddClubUser(ClubUser clubUser);
    Task AddClubUserRange(List<ClubUser> clubUsers);

    Task<bool> ClubJoined(int clubId, int userId);
    
    Task<List<ClubUser>?> GetClubUserRecords(int clubId, int userId);
    
    Task<List<ClubMinimalDto>?> GetJoinedClubsByIdAsync(int userId);
    Task<List<ClubMinimalDto>?> GetJoinedClubsByIdAsyncVer2(int userId);
    Task<List<ClubUser>?> GetClubUserRecordWithTracking(int clubId, int userId);
    void RemoveClubUserRange(List<ClubUser> clubUsers);

    Task<ClubUser?> TryRetrieveModeratorRole(int moderatorId);
    
    Task<List<ClubUserRoleDto>> GetModerators(int clubId);
    Task<List<UserDetailsDto>> GetModeratorsFull(int clubId);

}