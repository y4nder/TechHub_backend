using application.utilities.UserContext;
using domain.entities;
using domain.interfaces;
using MediatR;

namespace application.useCases.userInteractions.Queries.UserDetailsForEditQuery;

public record UserDetailsForEditQuery() : IRequest<UserDetailsForEditQueryResponse>;


public class UserDetailsForEditQueryResponse
{
    public string Message { get; set; } = null!;
    public UserDetailsDto UserDetails { get; set; } = null!;
}

public class UserDetailsQueryHandler : IRequestHandler<UserDetailsForEditQuery, UserDetailsForEditQueryResponse>
{
    private readonly IUserContext _context;
    private readonly IUserRepository _userRepository;

    public UserDetailsQueryHandler(IUserContext context, IUserRepository userRepository)
    {
        _context = context;
        _userRepository = userRepository;
    }

    public async Task<UserDetailsForEditQueryResponse> Handle(UserDetailsForEditQuery request, CancellationToken cancellationToken)
    {
        var userId = _context.GetUserId();
        
        var userDetails = await _userRepository.GetUserDetailsByIdAsync(userId);

        return new UserDetailsForEditQueryResponse
        {
            Message = "User Details retrieved",
            UserDetails = userDetails!
        };
    }
}