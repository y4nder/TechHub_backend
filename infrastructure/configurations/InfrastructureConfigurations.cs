using infrastructure.services.cloudinary;
using infrastructure.services.jwt;
using infrastructure.services.passwordService;
using infrastructure.services.worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace infrastructure.configurations;

public static class InfrastructureConfigurations
{
    public static IServiceCollection AddInfrastructureConfigurations(this IServiceCollection services,
        IConfiguration configuration)
    {
        //add db context
        services.AddDbContext<AppDbContext>(options 
            => options.UseSqlServer(configuration.GetConnectionString("connectionString"))
        );
        
        //add jwt settings
        services.AddJwtConfigurations(configuration);
        
        //add cloudinary settings
        services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

        //add services
        services.AddSingleton<IPasswordHasherService, PasswordHasherService>();
        services.AddSingleton<IJwtTokenProvider, JwtTokenProvider>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}