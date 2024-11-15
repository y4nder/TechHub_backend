namespace domain.entities;

public partial class Club
{
    public int ClubId { get; set; }

    public int? ClubCreatorId { get; set; }

    public string? ClubImageUrl { get; set; }

    public string? ClubName { get; set; }

    public string? ClubIntroduction { get; set; }

    public int? ClubCategoryId { get; set; }

    public short? PostPermission { get; set; }

    public short? InvitePermission { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual ClubCategory? ClubCategory { get; set; }

    public virtual User? ClubCreator { get; set; }

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
    public int ClubCategoryId { get; set; }
}

public enum PermissionType
{
    AllMembers = 0,
    Moderators = 1,
}
