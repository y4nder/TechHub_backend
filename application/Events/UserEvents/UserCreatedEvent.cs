using domain.entities;
using MediatR;

namespace application.Events.UserEvents;

public class UserCreatedEvent : INotification
{
    public UserCreatedEvent(User createdUser)
    {
        CreatedUser = createdUser;
    }

    public User CreatedUser { get; init; }
}