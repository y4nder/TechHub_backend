using application.configurations;
using infrastructure;
using infrastructure.configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace web_api.configurations;

public static class ServiceContainer
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationConfigurations();
        services.AddInfrastructureConfigurations(configuration);

        //add swagger auth

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "Auth",
                Version = "ASP.NET 8 Web API",
                Description = "Authenticatino with JWT"
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        // add authentication



        return services;
    }
}