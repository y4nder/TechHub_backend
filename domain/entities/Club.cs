namespace domain.entities;

public partial class Club
{
    public int ClubId { get; set; }

    public int ClubCreatorId { get; set; }

    public string? ClubImageUrl { get; set; }

    public string? ClubName { get; set; }

    public string? ClubIntroduction { get; set; }

    public int? ClubCategoryId { get; set; }

    public short? PostPermission { get; set; }

    public short? InvitePermission { get; set; }
    
    public int ClubViews { get; set; } = 0;

    public bool Featured { get; set; } = false;

    public bool Private { get; set; } = false;

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual ClubCategory? ClubCategory { get; set; }

    public virtual User? ClubCreator { get; set; }

    
    public virtual ClubAdditionalInfo? ClubAdditionalInfo { get; set; }
    
    public virtual ICollection<ClubUser> ClubUsers { get; set; } = new List<ClubUser>();

    public static Club CreateDefault(ClubDto dto)
    {
        return new Club
        {
            ClubCreatorId = dto.CreatorId,
            ClubImageUrl = dto.ImageUrl,
            ClubName = dto.ClubName,
            ClubIntroduction = dto.ClubIntroduction,
            ClubCategoryId = dto.ClubCategoryId,
            PostPermission = (short)PermissionType.AllMembers,
            InvitePermission = (short)PermissionType.AllMembers,
            Private = dto.IsPrivate,
        };
    }
    
    public static Club CreateCustom(ClubDto dto, int postPermission, int invitePermission)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto), "ClubDto cannot be null.");

        if (!Enum.IsDefined(typeof(PermissionType), postPermission))
        {
            throw new ArgumentException("Invalid postPermission value.");
        }

        if (!Enum.IsDefined(typeof(PermissionType), invitePermission))
        {
            throw new ArgumentException("Invalid invitePermission value.");
        }

        return new Club
        {
            ClubCreatorId = dto.CreatorId,
            ClubImageUrl = dto.ImageUrl,
            ClubName = dto.ClubName,
            ClubIntroduction = dto.ClubIntroduction,
            ClubCategoryId = dto.ClubCategoryId,
            PostPermission = (short)(PermissionType)postPermission,
            InvitePermission = (short)(PermissionType)invitePermission,
            Private = dto.IsPrivate,
        };
    }

    public static bool IsDefault(short postPermission, short invitePermission)
    {
        return postPermission == invitePermission 
               && invitePermission == 0;
    }
}

public class ClubDto
{
    public int CreatorId { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string ClubName { get; set; } = null!;
    public string? ClubIntroduction { get; set; }
    public int? ClubCategoryId { get; set; }
    public bool IsPrivate { get; set; }
}

public class ClubMinimalDto
{
    public int ClubId { get; set; }
    public string ClubProfilePicUrl { get; set; } = null!;
    public string ClubName { get; set; } = null!;
}

public enum PermissionType
{
    AllMembers = 0,
    Moderators = 1,
}

public class ClubStandardResponseDto
{
    public int ClubId { get; set; }
    public string ClubProfilePicUrl { get; set; } = null!;
    public string ClubName { get; set; } = null!;
    public string ClubDescription { get; set; } = null!;
    public int ClubMembersCount { get; set; } 
}
public class ClubFeaturedResponseDto
{
    public int ClubId { get; set; }
    public string ClubProfilePicUrl { get; set; } = null!;
    public string ClubName { get; set; } = null!;
    public string ClubDescription { get; set; } = null!;
    public int ClubMembersCount { get; set; }   
    public List<RecentMembersProfileResponseDto> RecentMembersProfilePics { get; set; } = new ();
}

public class RecentMembersProfileResponseDto
{
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public string UserProfilePicUrl { get; set; } = null!;

}

public class ClubCategoryStandardResponseDto
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
    public List<ClubStandardResponseDto> Clubs { get; set; } = new List<ClubStandardResponseDto>();
}

public class SingleCategoryClubStandardResponseDto
{
    public int? CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
    public List<ClubStandardResponseDto> Clubs { get; set; } = new();
}

public class SingleClubResponseDto
{
    public int ClubId { get; set; }
    public string ClubName { get; set; } = null!;
    public string ClubProfilePicUrl { get; set; } = null!;
    public DateTime? ClubCreatedDateTime { get; set; }
    public int PostCount { get; set; } = -1;
    public int ClubViews { get; set; } = -1;
    public int ClubUpVoteCount { get; set; } = -1;
    public bool Featured { get; set; } = false;
    public bool Private { get; set; } = false;
    public List<RecentMembersProfileResponseDto> RecentMemberProfilePics { get; set; } = new();
    public string ClubIntroduction { get; set; } = null!;
    public int MemberCount { get; set; } = -1;
    public ClubUserRoleDto ClubCreator { get; set; } = null!;
    public List<ClubUserRoleDto> Moderators { get; set; } = null!;
    public bool Joined { get; set; }
}

public class SuggestedClubDto
{
    public int ClubId { get; set; }
    public string ClubName { get; set; } = null!;
    public string ClubProfilePicUrl { get; set; } = null!;
}