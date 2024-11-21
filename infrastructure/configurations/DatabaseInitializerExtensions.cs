using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace infrastructure.configurations;

public static class DatabaseInitializerExtensions
{
    public static void InitializeDatabase(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Apply migrations
        dbContext.Database.Migrate();

        // Ensure the full-text catalog exists
        dbContext.EnsureFullTextCatalog();
    }
}