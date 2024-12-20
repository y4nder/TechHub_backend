using domain.entities;
using domain.interfaces;
using infrastructure.UserContext;
using MediatR;

namespace application.useCases.userInteractions.recommendedUsers;

public class RecommendedUsersQuery : IRequest<RecommendedUsersQueryResponse>
{
}

public class RecommendedUsersQueryResponse
{
    public List<UserFollowsListDto> Users { get; set; } = new();
}

public class RecommendedUsersQueryHandler : IRequestHandler<RecommendedUsersQuery, RecommendedUsersQueryResponse>
{
    private readonly IUserContext _userContext;
    private readonly IUserFollowRepository _userFollowRepository;

    public RecommendedUsersQueryHandler(IUserContext userContext, IUserFollowRepository userFollowRepository)
    {
        _userContext = userContext;
        _userFollowRepository = userFollowRepository;
    }

    public async Task<RecommendedUsersQueryResponse> Handle(RecommendedUsersQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();

        var recommendUsers = await _userFollowRepository.GetRecommendedUserFollowersByIdAsync(userId);

        return new RecommendedUsersQueryResponse
        {
            Users = recommendUsers
        };
    }
}