using infrastructure.configurations;

namespace web_api.configurations;

public static class WebApplicationExtensions
{
    public static void AddDatabaseConfigurations(this IServiceProvider serviceProvider)
    {
        serviceProvider.InitializeDatabase();
    } 
}