using domain.entities;

namespace domain.interfaces;

public interface IClubRepository
{
    Task<bool> CheckClubNameExists(string clubName);
    void AddNewClub(Club club);
}