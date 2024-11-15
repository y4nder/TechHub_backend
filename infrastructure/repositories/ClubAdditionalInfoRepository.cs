using domain.entities;
using domain.interfaces;

namespace infrastructure.repositories;

public class ClubAdditionalInfoRepository : IClubAdditionalInfoRepository
{
    private readonly AppDbContext _context;

    public ClubAdditionalInfoRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddClubAdditionalInfo(ClubAdditionalInfo clubAdditionalInfo)
    {
        _context.ClubAdditionalInfos.Add(clubAdditionalInfo);
    }
}