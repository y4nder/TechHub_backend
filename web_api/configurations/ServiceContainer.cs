using application.configurations;
using infrastructure;
using infrastructure.configurations;
using Microsoft.EntityFrameworkCore;

namespace web_api.configurations;

public static class ServiceContainer
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructureConfigurations(configuration);
        services.AddApplicationConfigurations();
   
        
        return services;
    }
}