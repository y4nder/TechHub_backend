using domain.entities;
using domain.interfaces;
using MediatR;

namespace application.useCases.clubInteractions.QueryClub.GetClubModerators;

public class ClubModeratorsFullQuery : IRequest<ClubModeratorsFullQueryResponse>
{
    public int ClubId { get; set; }
}

public class ClubModeratorsFullQueryResponse
{
    public string Message { get; set; } = null!;
    public List<UserDetailsDto> Moderators { get; set; } = new();
}

public class ClubModeratorsFullQueryHandler : IRequestHandler<ClubModeratorsFullQuery, ClubModeratorsFullQueryResponse>
{
    private readonly IClubUserRepository _clubUserRepository;

    public ClubModeratorsFullQueryHandler(IClubUserRepository clubUserRepository)
    {
        _clubUserRepository = clubUserRepository;
    }


    public async Task<ClubModeratorsFullQueryResponse> Handle(ClubModeratorsFullQuery request, CancellationToken cancellationToken)
    {
        var moderators = await _clubUserRepository.GetModeratorsFull(request.ClubId);

        return new ClubModeratorsFullQueryResponse
        {
            Moderators = moderators,
            Message = "Moderators full"
        };
    }
}