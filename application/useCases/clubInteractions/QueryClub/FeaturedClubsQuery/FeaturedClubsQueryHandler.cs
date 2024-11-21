using domain.interfaces;
using MediatR;

namespace application.useCases.clubInteractions.QueryClub.FeaturedClubsQuery;

public class FeaturedClubsQueryHandler : IRequestHandler<FeaturedClubsQuery, FeaturedClubsResponse>
{
    private readonly IClubRepository _clubRepository;

    public FeaturedClubsQueryHandler(IClubRepository clubRepository)
    {
        _clubRepository = clubRepository;
    }

    public async Task<FeaturedClubsResponse> Handle(FeaturedClubsQuery request, CancellationToken cancellationToken)
    {
        var featuredClubs = await _clubRepository.GetFeaturedClubsAsync();

        return new FeaturedClubsResponse
        {
            FeaturedClubs = featuredClubs
        };
    }
}