using domain.entities;
using domain.interfaces;
using MediatR;

namespace application.useCases.clubInteractions.QueryClub.GetClubModerators;

public class ClubModeratorsQuery : IRequest<ClubModeratorsQueryResponse>
{
    public int ClubId { get; set; }
}

public class ClubModeratorsQueryResponse
{
    public string Message { get; set; } = null!;
    public List<ClubUserRoleDto> Moderators { get; set; } = [];
}

public class ClubModeratorsQueryHandler : IRequestHandler<ClubModeratorsQuery, ClubModeratorsQueryResponse>
{
    private readonly IClubUserRepository _clubUserRepository;

    public ClubModeratorsQueryHandler(IClubUserRepository clubUserRepository)
    {
        _clubUserRepository = clubUserRepository;
    }

    public async Task<ClubModeratorsQueryResponse> Handle(ClubModeratorsQuery request, CancellationToken cancellationToken)
    {
        var moderators = await _clubUserRepository.GetModerators(request.ClubId);

        return new ClubModeratorsQueryResponse
        {
            Message = "Moderators retrieved",
            Moderators = moderators
        };
    }
}