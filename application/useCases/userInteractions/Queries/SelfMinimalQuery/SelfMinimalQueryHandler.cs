using domain.entities;
using domain.interfaces;
using MediatR;

namespace application.useCases.userInteractions.Queries.SelfMinimalQuery;

public class SelfMinimalQueryHandler : IRequestHandler<SelfMinimalQuery, UserMinimalDto>
{
    private readonly IUserRepository _userRepository;

    public SelfMinimalQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserMinimalDto> Handle(SelfMinimalQuery request, CancellationToken cancellationToken)
    {
        var userInfo = await _userRepository.GetMinimalUserByIdAsync(request.UserId)??
                       throw new KeyNotFoundException("User not found");

        return userInfo;
    }
}