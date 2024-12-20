using domain.entities;
using domain.interfaces;
using MediatR;

namespace application.useCases.clubInteractions.QueryClub.MinimalClubViewQuery;

public class MinimalClubViewQuery : IRequest<ClubInfoDto>
{
    public int ClubId { get; set; }
}

public class MinimalClubViewQueryHandler : IRequestHandler<MinimalClubViewQuery, ClubInfoDto>
{
    private readonly IClubRepository _clubRepository;

    public MinimalClubViewQueryHandler(IClubRepository clubRepository)
    {
        _clubRepository = clubRepository;
    }

    public async Task<ClubInfoDto> Handle(MinimalClubViewQuery request, CancellationToken cancellationToken)
    {
        var club = await _clubRepository.GetClubInfoByIdNoTracking(request.ClubId)??
                   throw new NullReferenceException("Club not found");

        return club;
    }
}