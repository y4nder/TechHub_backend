using domain.entities;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Data.Seeders;

public static class RoleSeeder
{
    public static void SeedRoles(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClubUserRole>(entity =>
        {
            entity.HasData(
                new ClubUserRole { RoleId = 1, RoleName= "Regular User"},
                new ClubUserRole { RoleId = 2, RoleName= "Club Creator"},
                new ClubUserRole { RoleId = 3, RoleName= "Moderator"},
                new ClubUserRole { RoleId = 4, RoleName= "Admin"}
            );
        });
    }
}

