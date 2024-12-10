using application.Events.ClubEvents;
using application.utilities.UserContext;
using domain.interfaces;
using MediatR;

namespace application.useCases.clubInteractions.QueryClub.SingleClubQuery;

public class SingleClubQueryHandler : IRequestHandler<SingleClubQuery, SingleClubQueryResponse>
{
    private readonly IClubRepository _clubRepository;
    private readonly IUserContext _userContext;
    private readonly IMediator _mediator;

    public SingleClubQueryHandler(IClubRepository clubRepository, IMediator mediator, IUserContext userContext)
    {
        _clubRepository = clubRepository;
        _mediator = mediator;
        _userContext = userContext;
    }

    public async Task<SingleClubQueryResponse> Handle(SingleClubQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();
        
        if(! await _clubRepository.ClubIdExists(request.ClubId) )
            throw new KeyNotFoundException("Club not found");
        
        await _mediator.Publish(new ClubQueriedEvent(request.ClubId), cancellationToken);
        
        var club = await _clubRepository.GetSingleClubByIdAsync(userId, request.ClubId);
        
        return new SingleClubQueryResponse
        {
            Message = "Club found",
            Club = club!
        };
    }
}