using application.Exceptions.FollowInteractionExceptions;
using application.utilities.UserContext;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.userInteractions.unfollowUser;

public class UnfollowUserCommandHandler : IRequestHandler<UnfollowUserCommand, UnfollowUserResponse>
{
    private readonly IUserFollowRepository _userFollowRepository;
    private readonly IUserContext _userContext;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UnfollowUserCommandHandler(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository, IUserFollowRepository userFollowRepository, IUserContext userContext)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _userFollowRepository = userFollowRepository;
        _userContext = userContext;
    }

    public async Task<UnfollowUserResponse> Handle(UnfollowUserCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();

        if (userId == request.FollowingId)
            throw FollowInteractionException.CannotUnfollowSelf();


        if (!await _userRepository.CheckIdExists(request.FollowingId))
            throw FollowInteractionException.FollowingIsNotFound();

        var userFollowRecord = await _userFollowRepository.GetUserFollow(userId, request.FollowingId) ??
                               throw FollowInteractionException.NotFollowed();

        _userFollowRepository.RemoveUserFollowRecord(userFollowRecord);

        await _unitOfWork.CommitAsync(cancellationToken);

        return new UnfollowUserResponse { Message = "Unfollowed User" };
    }
}