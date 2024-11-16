using application.Exceptions.TagExceptions;
using domain.entities;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.tagInteractions.FollowManyTags;

public class FollowManyTagsCommandHandler : IRequestHandler<FollowManyTagsCommand, FollowManyTagsResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IUserTagFollowRepository _userTagFollowRepository;
    private readonly IUnitOfWork _unitOfWork;

    public FollowManyTagsCommandHandler(IUserRepository userRepository, ITagRepository tagRepository,
        IUserTagFollowRepository userTagFollowRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _tagRepository = tagRepository;
        _userTagFollowRepository = userTagFollowRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<FollowManyTagsResponse> Handle(FollowManyTagsCommand request, CancellationToken cancellationToken)
    {
        //validate id
        var user = await _userRepository.GetUserById(request.UserId)??
                   throw TagException.UserIdNotFound(request.UserId);
        
        var ids = request.TagIdsToFollow;
        
        //validate tag ids
        if (ids.Count == 0)
        {
            throw TagException.ZeroFollowMany();
        }

        if (ids is [0] && ids.Count == 1)
        {
            throw TagException.ZeroFollowMany();
        }

        if (!TagsUnique(ids))
        {
            throw TagException.DuplicateTags();
        }

        if (!await EnsureAllTagsExist(ids))
        {
            throw TagException.InvalidTagIds();
        }
        
        //persist
        var userTagFollows = ids.Select(i => UserTagFollow.Create(user.UserId, i)).ToList();
        
        _userTagFollowRepository.AddRangeUserTagFollow(userTagFollows);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new FollowManyTagsResponse
        {
            Message = "tags have been followed",
        };
    }

    private bool TagsUnique(List<int> tagIds)
    {
        HashSet<int> seedTagIds = new HashSet<int>();
        bool isUnique = true;

        foreach (var number in tagIds)
        {
            if (!seedTagIds.Add(number))
            {
                isUnique = false;
                break;
            }
        }
        return isUnique;
    }

    private async Task<bool> EnsureAllTagsExist(List<int> tagIds)
    {
        return await _tagRepository.AreAllIdsValidAsync(tagIds);
    }
}