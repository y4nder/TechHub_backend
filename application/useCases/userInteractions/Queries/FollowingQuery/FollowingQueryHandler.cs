using domain.entities;
using domain.interfaces;
using domain.pagination;
using infrastructure.UserContext;
using MediatR;

namespace application.useCases.userInteractions.Queries.FollowingQuery;


public class FollowingQuery : IRequest<FollowingQueryResponse>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class FollowingQueryResponse
{
    public string Message { get; set; } = null!;
    public PaginatedResult<UserFollowsListDto> Users { get; set; } = new();

}

public class FollowingQueryHandler : IRequestHandler<FollowingQuery, FollowingQueryResponse>
{
    private readonly IUserContext _userContext;
    private readonly IUserFollowRepository _userFollowRepository;

    public FollowingQueryHandler(IUserContext userContext, IUserFollowRepository userFollowRepository)
    {
        _userContext = userContext;
        _userFollowRepository = userFollowRepository;
    }

    public async Task<FollowingQueryResponse> Handle(FollowingQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();

        var following = await _userFollowRepository.GetPaginatedFollowingByIdAsync(userId, request.PageNumber, request.PageSize);

        return new FollowingQueryResponse
        {
            Users = following,
            Message = "Following Retrived"
        };
    }
}
