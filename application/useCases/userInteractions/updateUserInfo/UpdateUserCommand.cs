using domain.entities;
using MediatR;

namespace application.useCases.userInteractions.updateUserInfo;

public class UpdateUserCommand : IRequest<UpdateUserCommandResponse>
{
    public string UserProfilePicUrl { get; set; } = null!;
    public string Username { get; set; } = null!;
    public UserAdditionalInfoDto UserInfo { get; set; } = new UserAdditionalInfoDto();
}

public class UpdateUserCommandResponse
{
    public string Message { get; set; } = null!;
}
