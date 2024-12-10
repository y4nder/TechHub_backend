using domain.interfaces;
using MediatR;

namespace application.useCases.userInteractions.Queries.UserFollowInfoQuery;

public class UserFollowInfoQueryHandler : IRequestHandler<UserFollowInfoQuery, UserFollowInfoResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserFollowRepository _userFollowRepository;

    public UserFollowInfoQueryHandler(IUserRepository userRepository, IUserFollowRepository userFollowRepository)
    {
        _userRepository = userRepository;
        _userFollowRepository = userFollowRepository;
    }

    public async Task<UserFollowInfoResponse> Handle(UserFollowInfoQuery request, CancellationToken cancellationToken)
    {
        if(! await _userRepository.CheckIdExists(request.UserId))
            throw new KeyNotFoundException("User not found");
        
        var followInfo = await _userFollowRepository.GetUserFollowInfo(request.UserId);

        return new UserFollowInfoResponse
        {
            Message = "Follower Info Retrieved",
            UserFollowInfo = followInfo
        };
    }
}