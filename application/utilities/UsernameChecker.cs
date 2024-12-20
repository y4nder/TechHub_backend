using domain.interfaces;
using MediatR;

namespace application.utilities;

public class UsernameChecker : IRequest<bool>
{
    public string Username { get; set; } = string.Empty;
}

public class UsernameCheckerHandler : IRequestHandler<UsernameChecker, bool>
{
    private readonly IUserRepository _userRepository;

    public UsernameCheckerHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(UsernameChecker request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Username))
            return await Task.FromResult(false);
        
        return !await _userRepository.CheckUserNameExists(request.Username);
    }
}