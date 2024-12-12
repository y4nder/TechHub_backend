using application.utilities.UserContext;
using domain.entities;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.clubInteractions.AssignModerator;

public class AssignModeratorCommand : IRequest<AssignModeratorCommandResponse>
{
    public int AssigneeId { get; set; }
    public int ClubId { get; set; }
}

public class AssignModeratorCommandResponse
{
    public string Message { get; set; } = null!;
}

public class AssignModeratorCommandHandler : IRequestHandler<AssignModeratorCommand, AssignModeratorCommandResponse>
{
    private readonly IUserContext _userContext;
    private readonly IClubUserRepository _clubUserRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AssignModeratorCommandHandler(IUserContext userContext, IClubUserRepository clubUserRepository, IUnitOfWork unitOfWork)
    {
        _userContext = userContext;
        _clubUserRepository = clubUserRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AssignModeratorCommandResponse> Handle(AssignModeratorCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();
        
        //check if userId is a club creator
        var invokerRecords = await _clubUserRepository.GetClubUserRecords(request.ClubId, userId);

        if (invokerRecords is null || !invokerRecords.Any())
        {
            throw new InvalidOperationException("User not found or user roles does not exist");
        }
        
        var invokerRoles = invokerRecords.Select(r => r.Role);

        if (!invokerRoles.Any(ir => ir!.RoleName == DefaultRoles.ClubCreator.ToStringFormat()))
        {
            // If no role is ClubCreator, execute this block
            throw new InvalidOperationException("Invoker is not a club creator");
        }
        
        // check if request user id is a valid userId
        var assigneeRecords = await _clubUserRepository.GetClubUserRecords(request.ClubId, request.AssigneeId)??
                              throw new InvalidOperationException("User not found or user roles does not exist");

        if(assigneeRecords.Count == 0)
            throw new InvalidOperationException("User not found or user roles does not exist");
        
        // check if request user id is not a moderator yet
        var assigneeRoles = assigneeRecords.Select(r => r.Role);

        if (assigneeRoles.Any(ir => ir!.RoleName == DefaultRoles.Moderator.ToStringFormat()))
        {
            throw new InvalidOperationException("AssigneeRoles is already a moderator");
        }
        
        // create record of moderator
        var clubUserRecord = ClubUser.CreateClubModerator(request.ClubId, request.AssigneeId);
        
        _clubUserRepository.AddClubUser(clubUserRecord);
        
        await _unitOfWork.CommitAsync(cancellationToken);

        return new AssignModeratorCommandResponse
        {
            Message = $"User {request.AssigneeId} assigned to club {request.ClubId}"
        };
    }
}