namespace domain.interfaces;

public interface IClubCategoryRepository
{
    Task<bool> CheckClubCategoryExists(int clubCategoryId);
}

