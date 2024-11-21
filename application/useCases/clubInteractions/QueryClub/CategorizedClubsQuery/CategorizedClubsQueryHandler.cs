using domain.interfaces;
using MediatR;

namespace application.useCases.clubInteractions.QueryClub.CategorizedClubsQuery;

public class CategorizedClubsQueryHandler : IRequestHandler<CategorizedClubsQuery, CategorizedClubsResponse>
{
    private readonly IClubRepository _clubRepository;

    public CategorizedClubsQueryHandler(IClubRepository clubRepository)
    {
        _clubRepository = clubRepository;
    }

    public async Task<CategorizedClubsResponse> Handle(CategorizedClubsQuery request, CancellationToken cancellationToken)
    {
        var clubs = await _clubRepository.GetAllCategorizedClubs();

        return new CategorizedClubsResponse
        {
            CategorizeClubs = clubs
        };
    }
}