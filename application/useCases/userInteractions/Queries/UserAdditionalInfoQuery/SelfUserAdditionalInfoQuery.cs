using domain.entities;
using MediatR;

namespace application.useCases.userInteractions.Queries.UserAdditionalInfoQuery;

public class SelfUserAdditionalInfoQuery : IRequest<UserAdditionalInfoResponse>
{
    public int UserId { get; set; }
}

public class UserAdditionalInfoResponse
{
    public required string Message { get; set; }
    public required UserProfileDto UserProfile { get; set; }
    public required UserAdditionalInfoDto UserAdditionalInfo { get; set; } 
}