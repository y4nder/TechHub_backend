using application.Events.ClubEvents;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.EventHandlers.ClubEventHandlers;

public class UpdateClubViewCountEventHandler : INotificationHandler<ClubQueriedEvent>
{
    private readonly IClubRepository _clubRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateClubViewCountEventHandler(IClubRepository clubRepository, IUnitOfWork unitOfWork)
    {
        _clubRepository = clubRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ClubQueriedEvent notification, CancellationToken cancellationToken)
    {
        int clubId = notification.QueriedClubId;

        var club = await _clubRepository.GetClubByIdNo(clubId);

        club!.ClubViews += 1;
        
        _clubRepository.UpdateClub(club);
        
        await _unitOfWork.CommitAsync(cancellationToken);
        
    }
}