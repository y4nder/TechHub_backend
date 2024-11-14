using domain.entities;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Data.Seeders;

public static class TagSeeder
{
    public static void SeedTags(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tag>().HasData(
            new Tag { TagId = 1, TagName = "C#" },
            new Tag { TagId = 2, TagName = "Python" },
            new Tag { TagId = 3, TagName = "JavaScript" },
            new Tag { TagId = 4, TagName = "Java" },
            new Tag { TagId = 5, TagName = "Ruby" },
            new Tag { TagId = 6, TagName = "SQL" },
            new Tag { TagId = 7, TagName = "C++" },
            new Tag { TagId = 8, TagName = "TypeScript" },
            new Tag { TagId = 9, TagName = "Go" },
            new Tag { TagId = 10, TagName = "Kotlin" },

            // Computer Science topics
            new Tag { TagId = 11, TagName = "Algorithms" },
            new Tag { TagId = 12, TagName = "Data Structures" },
            new Tag { TagId = 13, TagName = "Operating Systems" },
            new Tag { TagId = 14, TagName = "Machine Learning" },
            new Tag { TagId = 15, TagName = "Artificial Intelligence" },
            new Tag { TagId = 16, TagName = "Networking" },
            new Tag { TagId = 17, TagName = "Cybersecurity" },
            new Tag { TagId = 18, TagName = "Databases" },
            new Tag { TagId = 19, TagName = "Blockchain" },
            new Tag { TagId = 20, TagName = "Cloud Computing" }
        );


    }
}