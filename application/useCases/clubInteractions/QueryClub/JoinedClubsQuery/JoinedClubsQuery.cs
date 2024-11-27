using domain.entities;
using MediatR;

namespace application.useCases.clubInteractions.QueryClub.JoinedClubsQuery;

public record JoinedClubsQuery(int UserId) : IRequest<JoinedClubsResponse>;

public class JoinedClubsResponse
{
    public string Message { get; set; } = null!;
    public int ClubsJoinedCount { get; set; }
    public List<ClubMinimalDto> Clubs { get; set; } = null!;
}