using MediatR;

namespace application.useCases.clubInteractions.JoinClub;

public class JoinClubCommand : IRequest<JoinClubResponse>
{
    public int ClubId { get; set; }
    public int UserId { get; set; }
}

public class JoinClubResponse
{
    public string Message { get; set; } = null!;
}