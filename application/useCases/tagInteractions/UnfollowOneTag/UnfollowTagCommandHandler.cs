using application.Exceptions.TagExceptions;
using application.utilities.UserContext;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.tagInteractions.UnfollowOneTag;

public class UnfollowTagCommandHandler : IRequestHandler<UnfollowTagCommand, UnfollowTagResponse>
{
    private readonly IUserContext _userContext;
    private readonly ITagRepository _tagRepository;
    private readonly IUserTagFollowRepository _userTagFollowRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UnfollowTagCommandHandler(
        ITagRepository tagRepository,
        IUserTagFollowRepository userTagFollowRepository,
        IUnitOfWork unitOfWork, IUserContext userContext)
    {
        _tagRepository = tagRepository;
        _userTagFollowRepository = userTagFollowRepository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<UnfollowTagResponse> Handle(UnfollowTagCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();

        if (!await _tagRepository.TagIdExistsAsync(request.TagId))
        {
            throw TagException.TagIdNotFound(request.TagId);
        }
        
        var userTagFollowRecord = await _userTagFollowRepository.GetUserTagFollow(userId, request.TagId)??
                                  throw TagException.NotFollowed();
        
        _userTagFollowRepository.RemoveUserFollow(userTagFollowRecord);
        
        await _unitOfWork.CommitAsync(cancellationToken);

        return new UnfollowTagResponse
        {
            Message = "Tag Unfollowed"
        };
    }
    
}