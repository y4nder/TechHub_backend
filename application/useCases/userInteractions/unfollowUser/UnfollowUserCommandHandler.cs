using application.Exceptions.FollowInteractionExceptions;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.userInteractions.unfollowUser;

public class UnfollowUserCommandHandler : IRequestHandler<UnfollowUserCommand, UnfollowUserResponse>
{
    private readonly IUserFollowRepository _userFollowRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UnfollowUserCommandHandler(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository, IUserFollowRepository userFollowRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _userFollowRepository = userFollowRepository;
    }

    public async Task<UnfollowUserResponse> Handle(UnfollowUserCommand request, CancellationToken cancellationToken)
    {
        if (request.FollowerId == request.FollowingId)
            throw FollowInteractionException.CannotUnfollowSelf();

        if (!await _userRepository.CheckIdExists(request.FollowerId))
            throw FollowInteractionException.FollowerIsNotFound();

        if (!await _userRepository.CheckIdExists(request.FollowingId))
            throw FollowInteractionException.FollowingIsNotFound();

        var userFollowRecord = await _userFollowRepository.GetUserFollow(request.FollowerId, request.FollowingId) ??
                               throw FollowInteractionException.NotFollowed();

        _userFollowRepository.RemoveUserFollowRecord(userFollowRecord);

        await _unitOfWork.CommitAsync(cancellationToken);

        return new UnfollowUserResponse { Message = "Unfollowed User" };
    }
}