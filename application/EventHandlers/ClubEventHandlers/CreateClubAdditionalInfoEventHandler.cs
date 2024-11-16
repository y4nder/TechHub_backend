using application.Events.ClubEvents;
using domain.entities;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.EventHandlers.ClubEventHandlers;

public class CreateClubAdditionalInfoEventHandler : INotificationHandler<ClubCreatedEvent>
{
    private readonly IClubAdditionalInfoRepository _clubAdditionalInfoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateClubAdditionalInfoEventHandler(IClubAdditionalInfoRepository clubAdditionalInfoRepository,
        IUnitOfWork unitOfWork)
    {
        _clubAdditionalInfoRepository = clubAdditionalInfoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ClubCreatedEvent notification, CancellationToken cancellationToken)
    {
        var clubAdditionalInfo = ClubAdditionalInfo.CreateDefault(notification.CreatedClub);
        
        _clubAdditionalInfoRepository.AddClubAdditionalInfo(clubAdditionalInfo);
        
        await _unitOfWork.CommitAsync(cancellationToken); 
    }
}