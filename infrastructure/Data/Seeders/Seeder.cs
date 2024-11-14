using Microsoft.EntityFrameworkCore;

namespace infrastructure.Data.Seeders;

public static class Seeder
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.SeedClubCategories();
        modelBuilder.SeedTags();
        modelBuilder.SeedRoles();
    }
}