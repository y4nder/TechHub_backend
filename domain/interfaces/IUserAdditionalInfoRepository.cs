using domain.entities;

namespace domain.interfaces;

public interface IUserAdditionalInfoRepository
{
    void AddAdditionalInfoAsync(UserAdditionalInfo userAdditionalInfo);

    Task<UserAdditionalInfo?> GetAdditionalInfoAsync(int userId);
}