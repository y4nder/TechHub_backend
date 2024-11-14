using application.Exceptions.TagExceptions;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.tagInteractions.UnfollowOneTag;

public class UnfollowTagCommandHandler : IRequestHandler<UnfollowTagCommand, UnfollowTagResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IUserTagFollowRepository _userTagFollowRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UnfollowTagCommandHandler(IUserRepository userRepository,
        ITagRepository tagRepository,
        IUserTagFollowRepository userTagFollowRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _tagRepository = tagRepository;
        _userTagFollowRepository = userTagFollowRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UnfollowTagResponse> Handle(UnfollowTagCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.CheckIdExists(request.UserId))
        {
            throw TagException.UserIdNotFound(request.UserId);
        }

        if (!await _tagRepository.TagIdExistsAsync(request.TagId))
        {
            throw TagException.TagIdNotFound(request.TagId);
        }
        
        var userTagFollowRecord = await _userTagFollowRepository.GetUserTagFollow(request.UserId, request.TagId)??
                                  throw TagException.NotFollowed();
        
        _userTagFollowRepository.RemoveUserFollow(userTagFollowRecord);
        
        await _unitOfWork.CommitAsync(cancellationToken);

        return new UnfollowTagResponse
        {
            Message = "Tag Unfollowed"
        };
    }
    
}