using MediatR;

namespace application.useCases.tagInteractions.FollowManyTags;

public class FollowManyTagsCommand : IRequest<FollowManyTagsResponse>
{
    public int UserId { get; set; }
    public List<int> TagIdsToFollow { get; set; } = new();
}

public class FollowManyTagsResponse
{
    public string Message { get; set; } = null!;
}