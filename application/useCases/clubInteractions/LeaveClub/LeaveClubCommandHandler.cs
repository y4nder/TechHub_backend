using domain.interfaces;
using infrastructure.services.worker;
using infrastructure.UserContext;
using MediatR;

namespace application.useCases.clubInteractions.LeaveClub;

public class LeaveClubCommandHandler : IRequestHandler<LeaveClubCommand, LeaveClubResponse>
{
    private readonly IUserContext _userContext;
    private readonly IClubRepository _clubRepository;
    private readonly IClubUserRepository _clubUserRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LeaveClubCommandHandler(
        IUserContext userContext,
        IClubRepository clubRepository,
        IClubUserRepository clubUserRepository,
        IUnitOfWork unitOfWork)
    {
        _userContext = userContext;
        _clubRepository = clubRepository;
        _clubUserRepository = clubUserRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<LeaveClubResponse> Handle(LeaveClubCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();

        if (!await _clubRepository.ClubIdExists(request.ClubId))
            throw new KeyNotFoundException("Club not found");
        
        var roles = await _clubUserRepository.GetClubUserRecordWithTracking(request.ClubId, userId);
        
        if(roles is null || !roles.Any() )
            throw new InvalidOperationException("User is not part of this club");
        
        _clubUserRepository.RemoveClubUserRange(roles);
        
        await _unitOfWork.CommitAsync(cancellationToken);

        return new LeaveClubResponse
        {
            Message = "Success",
        };
    }
}
