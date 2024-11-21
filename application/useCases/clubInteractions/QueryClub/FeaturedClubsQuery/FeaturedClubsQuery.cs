using domain.entities;
using MediatR;

namespace application.useCases.clubInteractions.QueryClub.FeaturedClubsQuery;

public record FeaturedClubsQuery : IRequest<FeaturedClubsResponse>;

public class FeaturedClubsResponse
{
    public List<ClubFeaturedResponseDto> FeaturedClubs { get; set; } = null!;
}