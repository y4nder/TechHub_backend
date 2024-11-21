using domain.entities;
using MediatR;

namespace application.useCases.clubInteractions.QueryClub.SingleClubQuery;

public record SingleClubQuery(
    int ClubId) : IRequest<SingleClubQueryResponse>;

public class SingleClubQueryResponse
{
    public string Message { get; set; } = null!;
    public SingleClubResponseDto Club { get; set; } = null!;
}