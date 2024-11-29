using domain.entities;
using MediatR;

namespace application.Events.ClubEvents;

public class ClubQueriedEvent(int requestClubId) : INotification
{
    public int QueriedClubId { get; set; } = requestClubId;
}

