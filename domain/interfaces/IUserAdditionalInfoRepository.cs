using domain.entities;

namespace domain.interfaces;

public interface IUserAdditionalInfoRepository
{
    void AddAdditionalInfoAsync(UserAdditionalInfo userAdditionalInfo);

    Task<UserAdditionalInfoDto?> GetAdditionalInfoAsync(int userId);

    Task<UserAdditionalInfo?> GetUserAdditionalInfoForUpdateAsync(int userId);

    void UpdateAdditionalInfo(UserAdditionalInfo userAdditionalInfo);
}