﻿using domain.entities;

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
}