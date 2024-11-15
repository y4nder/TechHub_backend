using application.Exceptions.FollowInteractionExceptions;
using domain.entities;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.userInteractions.followUser;

public class FollowUserCommandHandler : IRequestHandler<FollowUserCommand, FollowUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserFollowRepository _userFollowRepository;
    private readonly IUnitOfWork _unitOfWork;

    public FollowUserCommandHandler(IUserRepository userRepository,
        IUserFollowRepository userFollowRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _userFollowRepository = userFollowRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<FollowUserResponse> Handle(FollowUserCommand request, CancellationToken cancellationToken)
    {
        if (request.FollowerId == request.FollowingId)
            throw FollowInteractionException.CannotFollowSelf();
        
        if (!await _userRepository.CheckIdExists(request.FollowerId))
            throw FollowInteractionException.FollowerIsNotFound();       
        
        if (!await _userRepository.CheckIdExists(request.FollowingId)) 
            throw FollowInteractionException.FollowingIsNotFound();
        
        if (await _userFollowRepository.CheckUserFollowRecord(request.FollowerId, request.FollowingId))
            throw FollowInteractionException.AlreadyFollowed();
        
        var userFollow = UserFollow.Create(request.FollowerId, request.FollowingId);
        
        _userFollowRepository.AddUserFollowRecord(userFollow);
        
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return new FollowUserResponse { Message = "user followed" };
    }
}