using MediatR;

namespace application.useCases.authentications.LoginUser;

public class LoginUserCommand : IRequest<LoginUserResponse>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class LoginUserResponse
{
    public string Token { get; set; } = null!;
    public string Message { get; set; } = null!;

    public string UserProfilePicUrl { get; set; } = null!;
    public int ReputationPoints { get; set; }
}