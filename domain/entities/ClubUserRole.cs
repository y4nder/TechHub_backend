namespace domain.entities;

public partial class ClubUserRole
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<ClubUser> ClubUsers { get; set; } = new List<ClubUser>();
}
