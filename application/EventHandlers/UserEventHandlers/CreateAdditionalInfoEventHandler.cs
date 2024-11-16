using application.Events.UserEvents;
using domain.entities;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.EventHandlers.UserEventHandlers;

public class CreateAdditionalInfoEventHandler : INotificationHandler<UserCreatedEvent>
{
    private readonly IUserAdditionalInfoRepository _userAdditionalInfoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAdditionalInfoEventHandler(IUserAdditionalInfoRepository userAdditionalInfoRepository,
        IUnitOfWork unitOfWork)
    {
        _userAdditionalInfoRepository = userAdditionalInfoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        var user = notification.CreatedUser;
        
        var userAdditionalInfo = UserAdditionalInfo.Create(user);
        
        _userAdditionalInfoRepository.AddAdditionalInfoAsync(userAdditionalInfo);
        
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}