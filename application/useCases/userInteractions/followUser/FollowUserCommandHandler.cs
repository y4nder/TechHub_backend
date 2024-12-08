using application.Exceptions.FollowInteractionExceptions;
using application.utilities.UserContext;
using domain.entities;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;
using System.Net.WebSockets;

namespace application.useCases.userInteractions.followUser;

public class FollowUserCommandHandler : IRequestHandler<FollowUserCommand, FollowUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserContext _userContext;
    private readonly IUserFollowRepository _userFollowRepository;
    private readonly IUnitOfWork _unitOfWork;

    public FollowUserCommandHandler(IUserRepository userRepository,
        IUserFollowRepository userFollowRepository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _userRepository = userRepository;
        _userFollowRepository = userFollowRepository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<FollowUserResponse> Handle(FollowUserCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();
        
        if (userId == request.FollowingId)
            throw FollowInteractionException.CannotFollowSelf();
        
        if (!await _userRepository.CheckIdExists(request.FollowingId)) 
            throw FollowInteractionException.FollowingIsNotFound();
        
        if (await _userFollowRepository.CheckUserFollowRecord(userId, request.FollowingId))
            throw FollowInteractionException.AlreadyFollowed();
        
        var userFollow = UserFollow.Create(userId, request.FollowingId);
        
        _userFollowRepository.AddUserFollowRecord(userFollow);
        
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return new FollowUserResponse { Message = "user followed" };
    }
}