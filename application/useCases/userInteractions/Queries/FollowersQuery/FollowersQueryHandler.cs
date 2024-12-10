using application.utilities.UserContext;
using domain.entities;
using domain.interfaces;
using domain.pagination;
using MediatR;

namespace application.useCases.userInteractions.Queries.FollowersQuery;

public class FollowersQuery : IRequest<FollowersQueryResponse>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class FollowersQueryResponse
{
    public string Message { get; set; } = null!;
    public PaginatedResult<UserFollowsListDto> Users { get; set; } = new();

}


public class FollowersQueryHandler : IRequestHandler<FollowersQuery, FollowersQueryResponse>
{

    private readonly IUserContext _userContext;
    private readonly IUserFollowRepository _userFollowRepository;

    public FollowersQueryHandler(IUserContext userContext, IUserFollowRepository userFollowRepository)
    {
        _userContext = userContext;
        _userFollowRepository = userFollowRepository;
    }

    public async Task<FollowersQueryResponse> Handle(FollowersQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();

        var followers = await _userFollowRepository.GetPaginatedFollowersByIdAsync(userId, request.PageNumber, request.PageSize);

        return new FollowersQueryResponse
        {
            Message = "followers retrieved",
            Users = followers,
        };
    }
}
