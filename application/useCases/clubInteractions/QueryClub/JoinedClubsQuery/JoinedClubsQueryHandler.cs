using domain.interfaces;
using MediatR;

namespace application.useCases.clubInteractions.QueryClub.JoinedClubsQuery;

public class JoinedClubsQueryHandler : IRequestHandler<JoinedClubsQuery, JoinedClubsResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IClubUserRepository _clubUserRepository;

    public JoinedClubsQueryHandler(IUserRepository userRepository, IClubUserRepository clubUserRepository)
    {
        _userRepository = userRepository;
        _clubUserRepository = clubUserRepository;
    }

    public async Task<JoinedClubsResponse> Handle(JoinedClubsQuery request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.CheckIdExists(request.UserId))
            throw new KeyNotFoundException("User not found");

        var clubs = await _clubUserRepository.GetJoinedClubsByIdAsync(request.UserId);

        return new JoinedClubsResponse
        {
            Message = "Joined clubs",
            ClubsJoinedCount = clubs?.Count ?? 0,
            Clubs = clubs?? new(),
        };
    }
}