using domain.entities;
using domain.interfaces;

namespace infrastructure.repositories;

public class UserAdditionalInfoRepository : IUserAdditionalInfoRepository
{
    private readonly AppDbContext _context;

    public UserAdditionalInfoRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddAdditionalInfoAsync(UserAdditionalInfo userAdditionalInfo)
    {
        _context.UserAdditionalInfos.Add(userAdditionalInfo);
    }
}