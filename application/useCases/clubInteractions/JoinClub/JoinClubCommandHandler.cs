using domain.entities;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.clubInteractions.JoinClub;

public class JoinClubCommandHandler : IRequestHandler<JoinClubCommand, JoinClubResponse>
{
    private readonly IClubRepository _clubRepository;
    private readonly IUserRepository _userRepository;
    private readonly IClubUserRepository _clubUserRepository;
    private readonly IUnitOfWork _unitOfWork;

    public JoinClubCommandHandler(IClubRepository clubRepository,
        IUserRepository userRepository,
        IClubUserRepository clubUserRepository,
        IUnitOfWork unitOfWork)
    {
        _clubRepository = clubRepository;
        _userRepository = userRepository;
        _clubUserRepository = clubUserRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<JoinClubResponse> Handle(JoinClubCommand request, CancellationToken cancellationToken)
    {
        if (!await _clubRepository.ClubIdExists(request.ClubId))
            throw new KeyNotFoundException("Club not found");
        
        if (! await _userRepository.CheckIdExists(request.UserId))
            throw new KeyNotFoundException("User not found");

        if (await _clubUserRepository.ClubJoined(request.ClubId, request.UserId))
            throw new InvalidOperationException("User is already joined");
        
        
        // var clubUser = ClubUser.Create(request.ClubId, request.UserId, (int)DefaultRoles.RegularUser);
        var clubUser = ClubUser.CreateClubRegularUser(request.ClubId, request.UserId);
        
        _clubUserRepository.AddClubUser(clubUser);
        
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return new JoinClubResponse { Message = "club joined"};
    }
}