using application.utilities.UserContext;
using domain.interfaces;
using MediatR;

namespace application.useCases.userInteractions.Queries.UserAdditionalInfoQuery;

public class UserAdditionalInfoQueryHandler : IRequestHandler<SelfUserAdditionalInfoQuery, UserAdditionalInfoResponse>
{
    
    private readonly IUserContext _userContext;
    private readonly IUserRepository _userRepository;
    private readonly IUserAdditionalInfoRepository _userAdditionalInfoRepository;

    public UserAdditionalInfoQueryHandler(IUserAdditionalInfoRepository userAdditionalInfoRepository, IUserContext userContext, IUserRepository userRepository)
    {
        _userAdditionalInfoRepository = userAdditionalInfoRepository;
        _userContext = userContext;
        _userRepository = userRepository;
    }

    public async Task<UserAdditionalInfoResponse> Handle(SelfUserAdditionalInfoQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();

        var profile = await _userRepository.GetUserProfileByIdAsync(userId) ??
                      throw new KeyNotFoundException("User not found");
        var info = await _userAdditionalInfoRepository.GetAdditionalInfoAsync(userId) ??
                   throw new KeyNotFoundException("User not found");
        
        return new UserAdditionalInfoResponse
        {
            Message = "User additional info retrieved",
            UserProfile = profile!,
            UserAdditionalInfo = info
        };
    }
}