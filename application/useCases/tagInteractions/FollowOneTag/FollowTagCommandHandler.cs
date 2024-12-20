using application.Exceptions.TagExceptions;
using domain.entities;
using domain.interfaces;
using infrastructure.services.worker;
using infrastructure.UserContext;
using MediatR;

namespace application.useCases.tagInteractions.FollowOneTag;

public class FollowTagCommandHandler : IRequestHandler<FollowTagCommand, FollowTagResponse>
{
    private readonly IUserTagFollowRepository _userTagFollowRepository;
    private readonly IUserContext _userContext;
    private readonly ITagRepository _tagRepository;
    private readonly IUnitOfWork _unitOfWork;

    public FollowTagCommandHandler(IUserTagFollowRepository userTagFollowRepository, 
        IUnitOfWork unitOfWork, ITagRepository tagRepository, IUserContext userContext)
    {
        _userTagFollowRepository = userTagFollowRepository;
        _unitOfWork = unitOfWork;
        _tagRepository = tagRepository;
        _userContext = userContext;
    }

    public async Task<FollowTagResponse> Handle(FollowTagCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();

        if (!await _tagRepository.TagIdExistsAsync(request.TagId))
        {
            throw TagException.TagIdNotFound(request.TagId);
        }
        
        await EnsureTagNotFollowed(userId, request.TagId);
        
        var userTagFollow = UserTagFollow.Create(userId, request.TagId);
        
        _userTagFollowRepository.AddUserFollow(userTagFollow);
        
        await _unitOfWork.CommitAsync(cancellationToken);

        return new FollowTagResponse
        {
            Message = "tag followed"
        };
    }

    private async Task EnsureTagNotFollowed(int requestUserId, int requestTagId)
    {
        if (await _userTagFollowRepository.CheckUserFollow(requestUserId, requestTagId))
            throw TagException.AlreadyFollowed();
    }
}