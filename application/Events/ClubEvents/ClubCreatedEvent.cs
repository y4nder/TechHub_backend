using domain.entities;
using MediatR;

namespace application.Events.ClubEvents;

public class ClubCreatedEvent : INotification
{
    public Club CreatedClub { get; set; }
}

