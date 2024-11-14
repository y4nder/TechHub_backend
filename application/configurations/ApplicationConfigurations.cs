using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace application.configurations;

public static class ApplicationConfigurations
{
    public static IServiceCollection AddApplicationConfigurations(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ApplicationAssemblyMarker>());
        services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyMarker>();
        services.AddRepositoryConfigurations();
        
        return services;    
    }
}