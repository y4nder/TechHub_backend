using application.Events.ClubEvents;
using domain.entities;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.EventHandlers.ClubEventHandlers;

public class CreateClubUserEntryOnCreationEventHandler : INotificationHandler<ClubCreatedEvent>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClubUserRepository _clubUserRepository;

    public CreateClubUserEntryOnCreationEventHandler(IUnitOfWork unitOfWork, IClubUserRepository clubUserRepository)
    {
        _unitOfWork = unitOfWork;
        _clubUserRepository = clubUserRepository;
    }

    public async Task Handle(ClubCreatedEvent notification, CancellationToken cancellationToken)
    {
        var createdClub = notification.CreatedClub;

        var clubModerator = new ClubUser
        {
            ClubId = createdClub.ClubId,
            UserId = createdClub.ClubCreatorId,
            RoleId = ClubUserRole.CreateDefaultRole(DefaultRoles.Moderator).RoleId
        };

        var clubCreator = new ClubUser
        {
            ClubId = createdClub.ClubId,
            UserId = createdClub.ClubCreatorId,
            RoleId = ClubUserRole.CreateDefaultRole(DefaultRoles.ClubCreator).RoleId
        };
        
        await _clubUserRepository.AddClubUserRange([clubModerator, clubCreator]);
        
        await _unitOfWork.CommitAsync(cancellationToken);            
    }
}