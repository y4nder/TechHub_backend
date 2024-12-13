using domain.entities;
using domain.interfaces;

namespace application.useCases.ModeratorInteractions.Shared;

public class RoleChecker
{
    private readonly IClubUserRepository _clubUserRepository;

    public RoleChecker(IClubUserRepository clubUserRepository)
    {
        _clubUserRepository = clubUserRepository;
    }

    public async Task CheckForRole(int id, int clubId, DefaultRoles role)
    {
        var invokerRecords = await _clubUserRepository
            .GetClubUserRecords(clubId, id);

        if (invokerRecords is null || !invokerRecords.Any())
        {
            throw new InvalidOperationException("User not found or user roles does not exist");
        }
        
        var invokerRoles = invokerRecords.Select(r => r.Role);

        if (!invokerRoles.Any(ir => ir!.RoleName == role.ToStringFormat()))
        {
            // If no role is ClubCreator, execute this block
            throw new InvalidOperationException($"Invoker is not a {role.ToStringFormat()} role");
        }
    }
}