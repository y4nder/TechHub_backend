using domain.interfaces;
using MediatR;

namespace application.useCases.userInteractions.Queries.UserAdditionalInfoQuery;

public class UserAdditionalInfoQueryHandler : IRequestHandler<SelfUserAdditionalInfoQuery, UserAdditionalInfoResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserAdditionalInfoRepository _userAdditionalInfoRepository;

    public UserAdditionalInfoQueryHandler(IUserRepository userRepository, IUserAdditionalInfoRepository userAdditionalInfoRepository)
    {
        _userRepository = userRepository;
        _userAdditionalInfoRepository = userAdditionalInfoRepository;
    }

    public async Task<UserAdditionalInfoResponse> Handle(SelfUserAdditionalInfoQuery request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.CheckIdExists(request.UserId))
            throw new KeyNotFoundException("User not found");

        var profile = await _userRepository.GetUserProfileByIdAsync(request.UserId) ??
                      throw new KeyNotFoundException("User not found");
        var info = await _userAdditionalInfoRepository.GetAdditionalInfoAsync(request.UserId) ??
                   throw new KeyNotFoundException("User not found");
        
        return new UserAdditionalInfoResponse
        {
            Message = "User additional info retrieved",
            UserProfile = profile!,
            UserAdditionalInfo = info
        };
    }
}