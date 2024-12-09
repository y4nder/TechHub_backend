using application.utilities.UserContext;
using domain.entities;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.clubInteractions.JoinClub;

public class JoinClubCommandHandler : IRequestHandler<JoinClubCommand, JoinClubResponse>
{
    private readonly IUserContext _userContext; 
    private readonly IClubRepository _clubRepository;
    private readonly IClubUserRepository _clubUserRepository;
    private readonly IUnitOfWork _unitOfWork;

    public JoinClubCommandHandler(
        IClubRepository clubRepository,
        IClubUserRepository clubUserRepository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _clubRepository = clubRepository;
        _clubUserRepository = clubUserRepository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<JoinClubResponse> Handle(JoinClubCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();
        
        if (!await _clubRepository.ClubIdExists(request.ClubId))
            throw new KeyNotFoundException("Club not found");
        
        if (await _clubUserRepository.ClubJoined(request.ClubId, userId))
            throw new InvalidOperationException("User is already joined");

        var clubUser = ClubUser.CreateClubRegularUser(request.ClubId, userId);
        
        _clubUserRepository.AddClubUser(clubUser);
        
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return new JoinClubResponse { Message = "club joined"};
    }
}