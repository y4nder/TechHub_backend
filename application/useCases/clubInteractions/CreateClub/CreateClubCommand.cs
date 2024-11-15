using MediatR;
using Microsoft.AspNetCore.Http;

namespace application.useCases.clubInteractions.CreateClub;

public class CreateClubCommand : IRequest<CreateClubResponse>
{
    public int CreatorId { get; set; }
    public IFormFile ClubThumbnail { get; set; } = null!;
    public string ClubName { get; set; } = null!;
    public string? ClubIntroduction { get; set; } = null!;
    public int ClubCategoryId { get; set; }
    public short PostPermission { get; set; } = -1;

    public short InvitePermission { get; set; } = -1;
}

public class CreateClubResponse
{
    public string Message { get; set; } = null!;
}