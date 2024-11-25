using application.Events.ClubEvents;
using domain.interfaces;
using MediatR;

namespace application.useCases.clubInteractions.QueryClub.SingleClubQuery;

public class SingleClubQueryHandler : IRequestHandler<SingleClubQuery, SingleClubQueryResponse>
{
    private readonly IClubRepository _clubRepository;
    private readonly IMediator _mediator;

    public SingleClubQueryHandler(IClubRepository clubRepository, IMediator mediator)
    {
        _clubRepository = clubRepository;
        _mediator = mediator;
    }

    public async Task<SingleClubQueryResponse> Handle(SingleClubQuery request, CancellationToken cancellationToken)
    {
        if(! await _clubRepository.ClubIdExists(request.ClubId) )
            throw new KeyNotFoundException("Club not found");
        
        await _mediator.Publish(new ClubQueriedEvent(request.ClubId), cancellationToken);
        
        var club = await _clubRepository.GetSingleClubByIdAsync(request.ClubId);
        
        return new SingleClubQueryResponse
        {
            Message = "Club found",
            Club = club!
        };
    }
}