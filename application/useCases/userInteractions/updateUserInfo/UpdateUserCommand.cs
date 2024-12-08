using domain.entities;
using MediatR;

namespace application.useCases.userInteractions.updateUserInfo;

public class UpdateUserCommand : IRequest<UpdateUserCommandResponse>
{
    public UserAdditionalInfoDto UserInfo { get; set; } = new UserAdditionalInfoDto();
}

public class UpdateUserCommandResponse
{
    public string Message { get; set; } = null!;
}
