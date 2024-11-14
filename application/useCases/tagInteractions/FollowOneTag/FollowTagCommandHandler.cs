using application.Exceptions.TagExceptions;
using domain.entities;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.tagInteractions.FollowOneTag;

public class FollowTagCommandHandler : IRequestHandler<FollowTagCommand, FollowTagResponse>
{
    private readonly IUserTagFollowRepository _userTagFollowRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IUnitOfWork _unitOfWork;

    public FollowTagCommandHandler(IUserTagFollowRepository userTagFollowRepository, 
        IUserRepository userRepository,
        IUnitOfWork unitOfWork, ITagRepository tagRepository)
    {
        _userTagFollowRepository = userTagFollowRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _tagRepository = tagRepository;
    }

    public async Task<FollowTagResponse> Handle(FollowTagCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.CheckIdExists(request.UserId))
        {
            throw TagException.UserIdNotFound(request.UserId);
        }

        if (!await _tagRepository.TagIdExistsAsync(request.TagId))
        {
            throw TagException.TagIdNotFound(request.TagId);
        }
        
        await EnsureTagNotFollowed(request.UserId, request.TagId);
        
        var userTagFollow = UserTagFollow.Create(request.UserId, request.TagId);
        
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