using domain.entities;

namespace domain.interfaces;

public interface IClubRepository
{
    Task<bool> CheckClubNameExists(string clubName);
    void AddNewClub(Club club);
    Task<bool> ClubIdExists(int clubId);
    
    Task<Club?> GetClubByIdNoTracking(int clubId);

    Task<List<ClubCategoryStandardResponseDto>> GetAllCategorizedClubs();
    Task<List<ClubFeaturedResponseDto>> GetFeaturedClubsAsync();
    Task<SingleClubResponseDto?> GetSingleClubByIdAsync(int userId, int clubId);
    Task<Club?> GetClubByIdNo(int clubId);
    
    void UpdateClub(Club club);
    
    Task<List<ClubMinimalDto>?> GetJoinedClubsByIdAsync(int userId);  
    
    Task<SingleCategoryClubStandardResponseDto?> GetSingleCategoryClubByIdAsync(int clubCategoryId);
    
    Task<List<SuggestedClubDto>> GetSuggestedClubs(string searchTerm);
    
    Task<List<ClubUserMinimalDto>> GetClubUsers(int clubId, string searchTerm);
    Task<int> GetClubUsersCount(int clubId);

    Task UpdateUserRolesTransaction(List<ClubUser> userRoles, int clubId, int userId);
    
    Task<ClubForEditDto?> GetClubForEdit(int clubId);
}