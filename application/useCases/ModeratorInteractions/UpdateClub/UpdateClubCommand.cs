using application.useCases.ModeratorInteractions.Shared;
using application.utilities.UserContext;
using domain.entities;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.ModeratorInteractions.UpdateClub;

public class UpdateClubCommand : IRequest<UpdateClubCommandResponse>
{
    public int ClubId { get; set; }
    public ClubUpdateDto ClubUpdateInfo { get; set; } = new();
}

public class UpdateClubCommandResponse
{
    public string Message { get; set; } = null!;
}

public class UpdateClubCommandHandler : IRequestHandler<UpdateClubCommand, UpdateClubCommandResponse>
{
    private readonly IUserContext _userContext;
    private readonly IClubRepository _clubRepository;
    private readonly RoleChecker _roleChecker;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateClubCommandHandler(
        IUserContext userContext, 
        IClubRepository clubRepository, 
        RoleChecker roleChecker, 
        IUnitOfWork unitOfWork)
    {
        _userContext = userContext;
        _clubRepository = clubRepository;
        _roleChecker = roleChecker;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateClubCommandResponse> Handle(UpdateClubCommand request, CancellationToken cancellationToken)
    {
        var moderatorId = _userContext.GetUserId();
        
        await _roleChecker.CheckForRole(moderatorId, request.ClubId, DefaultRoles.Moderator);
        
        var club = await _clubRepository.GetClubByIdNo(request.ClubId)??
                   throw new KeyNotFoundException("Club not found"); 
        
        club.Update(request.ClubUpdateInfo);
        await _unitOfWork.CommitAsync(cancellationToken);
        return new UpdateClubCommandResponse
        {
            Message = "Updated club"
        };
    }
}