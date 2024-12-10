using application.utilities.UserContext;
using domain.interfaces;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.userInteractions.updateUserInfo;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserCommandResponse>
{
    private readonly IUserContext _userContext;
    private readonly IUserRepository _userRepository;
    private readonly IUserAdditionalInfoRepository _additionalInfoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(
        IUserContext userContext,
        IUserAdditionalInfoRepository additionalInfoRepository,
        IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _userContext = userContext;
        _additionalInfoRepository = additionalInfoRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();

        if (await _userRepository.CheckUserNameExists(request.Username) && request.Username != await _userRepository.GetUserNameByIdAsync(userId))
            throw new InvalidOperationException($"Username {request.Username} already exists.");
        
        var user = await _userRepository.GetUserById(userId)??
                   throw new KeyNotFoundException("User not found");
        
        user.ProfileUpdate(request.UserProfilePicUrl, request.Username);
        
        var userInfoDto = request.UserInfo;
        
        var userInfo =  await _additionalInfoRepository.GetUserAdditionalInfoForUpdateAsync(userId)??
            throw new KeyNotFoundException("User record not found");

        userInfo.Update(userInfoDto);

        _additionalInfoRepository.UpdateAdditionalInfo(userInfo);

        await _unitOfWork.CommitAsync(cancellationToken);

        return new UpdateUserCommandResponse
        {
            Message = $"user: {userId} was updated"
        };
    }
}
