namespace domain.entities;

public partial class ClubUser
{
    public int ClubId { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    public virtual Club Club { get; set; } = null!;

    public virtual ClubUserRole? Role { get; set; }

    public virtual User User { get; set; } = null!;

    public static ClubUser Create(int clubId, int userId, int roleId)
    {
        return new ClubUser
        {
            ClubId = clubId,
            UserId = userId,
            RoleId = roleId
        };
    }
}
