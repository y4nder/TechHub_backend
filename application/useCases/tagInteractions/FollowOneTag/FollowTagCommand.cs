using MediatR;

namespace application.useCases.tagInteractions.FollowOneTag;

public class FollowTagCommand : IRequest<FollowTagResponse>
{
    public int TagId { get; set; }
}

public class FollowTagResponse
{
    public string Message { get; set; } = null!;
}