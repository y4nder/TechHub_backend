using domain.entities;
using domain.interfaces;
using infrastructure.services.worker;
using infrastructure.UserContext;
using MediatR;

namespace application.useCases.clubInteractions.RemoveModerator;

public class RemoveModeratorCommand : IRequest<RemoveModeratorCommandResponse>
{
    public int ModeratorId { get; set; }
    public int ClubId { get; set; }
}

public class RemoveModeratorCommandResponse
{
    public string Message { get; set; } = null!;
}

public class RemoveModeratorCommandHandler : IRequestHandler<RemoveModeratorCommand, RemoveModeratorCommandResponse>
{
    private readonly IUserContext _userContext;
    private readonly IClubUserRepository _clubUserRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveModeratorCommandHandler(
        IUserContext userContext, 
        IClubUserRepository clubUserRepository, 
        IUnitOfWork unitOfWork)
    {
        _userContext = userContext;
        _clubUserRepository = clubUserRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<RemoveModeratorCommandResponse> Handle(RemoveModeratorCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();
        
        var invokerRecords = await _clubUserRepository.GetClubUserRecords(request.ClubId, userId);

        if (invokerRecords is null || !invokerRecords.Any())
        {
            throw new InvalidOperationException("invoker is not part of the club");
        }
        
        var invokerRoles = invokerRecords.Select(r => r.Role);

        if (!invokerRoles.Any(ir => ir!.RoleName == DefaultRoles.ClubCreator.ToStringFormat()))
        {
            // If no role is ClubCreator, execute this block
            throw new InvalidOperationException("Invoker is not a club creator");
        }
        
        // check if moderatorId is valid
        var moderatorRecord = await _clubUserRepository.TryRetrieveModeratorRole(request.ModeratorId)??
                              throw new KeyNotFoundException("user is not a moderator");
        
        _clubUserRepository.RemoveClubUserRange(new List<ClubUser>{moderatorRecord});
        
        await _unitOfWork.CommitAsync(cancellationToken);

        return new RemoveModeratorCommandResponse
        {
            Message = "Moderator has been removed."
        };
    }
}