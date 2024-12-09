using MediatR;

namespace application.useCases.clubInteractions.LeaveClub;

public class LeaveClubCommand : IRequest<LeaveClubResponse>
{
    public int ClubId { get; set; }
}

public class LeaveClubResponse
{
    public string Message { get; set; } = null!;
}
