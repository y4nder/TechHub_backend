using domain.entities;
using domain.interfaces;
using MediatR;

namespace application.useCases.ModeratorInteractions.ClubMembers;

public class ClubMembersQuery : IRequest<ClubMembersQueryResponse>
{
    public int ClubId { get; set; }
    public string SearchTerm { get; set; } = "";
}

public class ClubMembersQueryResponse
{
    public string Message { get; set; } = null!;
    public List<ClubUserMinimalDto> Members { get; set; } = new();
}

public class ClubMembersQueryHandler : IRequestHandler<ClubMembersQuery, ClubMembersQueryResponse>
{
    private readonly IClubRepository _clubRepository;

    public ClubMembersQueryHandler(IClubRepository clubRepository)
    {
        _clubRepository = clubRepository;
    }

    public async Task<ClubMembersQueryResponse> Handle(ClubMembersQuery request, CancellationToken cancellationToken)
    {
        var members = await _clubRepository.GetClubUsers(request.ClubId, request.SearchTerm);

        return new ClubMembersQueryResponse
        {
            Message = "Club Members found",
            Members = members
        };
    }
}
