using domain.entities;

namespace domain.interfaces;

public interface IClubCategoryRepository
{
    Task<bool> CheckClubCategoryExists(int clubCategoryId);
    
    Task<List<ClubCategoryDto>> GetAllClubCategoriesAsync();
}

