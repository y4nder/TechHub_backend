using domain.interfaces;
using MediatR;

namespace application.useCases.clubInteractions.QueryClub.SingleClubQuery;

public class SingleClubQueryHandler : IRequestHandler<SingleClubQuery, SingleClubQueryResponse>
{
    private readonly IClubRepository _clubRepository;

    public SingleClubQueryHandler(IClubRepository clubRepository)
    {
        _clubRepository = clubRepository;
    }

    public async Task<SingleClubQueryResponse> Handle(SingleClubQuery request, CancellationToken cancellationToken)
    {
        if(! await _clubRepository.ClubIdExists(request.ClubId) )
            throw new KeyNotFoundException("Club not found");
        
        var club = await _clubRepository.GetSingleClubByIdAsync(request.ClubId);
        
        return new SingleClubQueryResponse
        {
            Message = "Club found",
            Club = club!
        };
    }
}