namespace domain.entities;

public partial class ClubUserRole
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<ClubUser> ClubUsers { get; set; } = new List<ClubUser>();

    public static ClubUserRole CreateDefaultRole(DefaultRoles role)
    {
        switch (role)
        {
            case DefaultRoles.RegularUser:
                return new ClubUserRole { RoleId = 1, RoleName = "Regular User" };
            case DefaultRoles.ClubCreator:
                return new ClubUserRole { RoleId = 2, RoleName = "Club Creator" };
            case DefaultRoles.Moderator:
                return new ClubUserRole { RoleId = 3, RoleName = "Moderator" };
            case DefaultRoles.Admin:
                return new ClubUserRole { RoleId = 4, RoleName = "Admin" };
            default:
                throw new ArgumentException("Invalid role");
        }
    }
}

public enum DefaultRoles
{
   RegularUser = 1,
   ClubCreator = 2,
   Moderator = 3,
   Admin = 4
}

// new ClubUserRole { RoleId = 1, RoleName= "Regular User"},
// new ClubUserRole { RoleId = 2, RoleName= "Club Creator"},
// new ClubUserRole { RoleId = 3, RoleName= "Moderator"},
// new ClubUserRole { RoleId = 4, RoleName= "Admin"}
