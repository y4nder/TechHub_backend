using domain.entities;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Data.Seeders;

public static class ClubCategorySeeder 
{
    public static void SeedClubCategories(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClubCategory>(entity
            => entity.HasData(GetDefaultCategories())
        );
    }

    private static IEnumerable<ClubCategory> GetDefaultCategories()
    {
        return new List<ClubCategory>()
        {
            new() { ClubCategoryId = 1, ClubCategoryName = "Programming Languages" },
            new() { ClubCategoryId = 2, ClubCategoryName = "DevOps" },
            new() { ClubCategoryId = 3, ClubCategoryName = "Artificial Intelligence" },
            new() { ClubCategoryId = 4, ClubCategoryName = "Web Development" },
            new() { ClubCategoryId = 5, ClubCategoryName = "Mobile Development" },
            new() { ClubCategoryId = 6, ClubCategoryName = "Game Development" },
            new() { ClubCategoryId = 7, ClubCategoryName = "Data Science" },
            new() { ClubCategoryId = 8, ClubCategoryName = "Cybersecurity" },
            new() { ClubCategoryId = 9, ClubCategoryName = "Cloud Computing" },
            new() { ClubCategoryId = 10, ClubCategoryName = "Software Engineering" }

        };
    }
}