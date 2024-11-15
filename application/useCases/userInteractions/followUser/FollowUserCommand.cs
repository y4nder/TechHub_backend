using MediatR;

namespace application.useCases.userInteractions.followUser;

public class FollowUserCommand : IRequest<FollowUserResponse>
{
    public int FollowerId { get; set; }
    public int FollowingId { get; set; }
}

public class FollowUserResponse
{
    public string Message { get; set; } = null!;
}

