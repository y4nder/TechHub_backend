using domain.entities;
using domain.interfaces;
using MediatR;

namespace application.useCases.clubInteractions.QueryClub.ClubForEditQuery;

public class ClubForEditQuery : IRequest<ClubForEditDto>
{
    public int ClubId { get; set; }
}

public class ClubForEditQueryHandler : IRequestHandler<ClubForEditQuery, ClubForEditDto>
{
    private readonly IClubRepository _clubRepository;

    public ClubForEditQueryHandler(IClubRepository clubRepository)
    {
        _clubRepository = clubRepository;
    }

    public async Task<ClubForEditDto> Handle(ClubForEditQuery request, CancellationToken cancellationToken)
    {
        var club = await _clubRepository.GetClubForEdit(request.ClubId)??
                   throw new NullReferenceException("Club not found");
        
        return club;
    }
}