using domain.entities;
using MediatR;

namespace application.useCases.clubInteractions.QueryClub.CategorizedClubsQuery;

public record CategorizedClubsQuery() : IRequest<CategorizedClubsResponse>;

public class CategorizedClubsResponse
{
    public List<ClubCategoryStandardResponseDto> CategorizeClubs { get; set; } = null!;
}