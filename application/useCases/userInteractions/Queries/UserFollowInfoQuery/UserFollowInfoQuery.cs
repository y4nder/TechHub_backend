using domain.entities;
using MediatR;

namespace application.useCases.userInteractions.Queries.UserFollowInfoQuery;

public class UserFollowInfoQuery : IRequest<UserFollowInfoResponse>
{
    public int UserId { get; set; }
}

public class UserFollowInfoResponse
{
    public string Message { get; set; } = null!;
    public UserFollowInfoDto UserFollowInfo { get; set; } = new();
}