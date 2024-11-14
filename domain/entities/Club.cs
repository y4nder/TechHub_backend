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
}
