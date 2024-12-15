using domain.entities;
using domain.interfaces;
using MediatR;

namespace application.useCases.ModeratorInteractions.UpdateUserRoles;

public class UpdateUserRolesCommand : IRequest<UpdateUserRolesCommandResponse>
{
    public int UserId { get; set; }
    public int ClubId { get; set; }
    public List<int> RoleIds { get; set; } = new();
}

public class UpdateUserRolesCommandResponse
{
    public string Message { get; set; } = null!;
}

public class UpdateUserRolesCommandHandler : IRequestHandler<UpdateUserRolesCommand, UpdateUserRolesCommandResponse>
{
    private readonly IClubRepository _clubRepository;

    public UpdateUserRolesCommandHandler(IClubRepository clubRepository)
    {
        _clubRepository = clubRepository;
    }

    public async Task<UpdateUserRolesCommandResponse> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
    {
        List<ClubUser> roles = new();

        foreach (var id in request.RoleIds)
        {
            roles.Add(ClubUser.Create(request.ClubId, request.UserId, id));
        } 

        await _clubRepository.UpdateUserRolesTransaction(roles, request.ClubId, request.UserId);

        return new UpdateUserRolesCommandResponse
        {
            Message = $"Successfully updated {request.ClubId} to user {request.UserId}"
        };
    }
}