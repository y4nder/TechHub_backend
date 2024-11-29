using domain.entities;

namespace domain.interfaces;

public interface IClubUserRepository
{
    void AddClubUser(ClubUser clubUser);
    Task AddClubUserRange(List<ClubUser> clubUsers);

    Task<bool> ClubJoined(int clubId, int userId);
    
    Task<List<ClubUser>?> GetClubUserRecord(int clubId, int userId);
    
    Task<List<ClubMinimalDto>?> GetJoinedClubsByIdAsync(int userId);
}