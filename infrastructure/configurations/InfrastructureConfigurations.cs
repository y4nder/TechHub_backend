using System.Security.Claims;
using infrastructure.services.analytics;
using infrastructure.services.backgroundServices;
using infrastructure.services.cloudinary;
using infrastructure.services.httpImgInterceptor;
using infrastructure.services.jobs;
using infrastructure.services.jwt;
using infrastructure.services.passwordService;
using infrastructure.services.worker;
using infrastructure.UserContext;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;

namespace infrastructure.configurations;

public static class InfrastructureConfigurations
{
    public static IServiceCollection AddInfrastructureConfigurations(this IServiceCollection services,
        IConfiguration configuration)
    {
        //add db context
        services.AddDbContext<AppDbContext>(options 
            => options.UseSqlServer(configuration.GetConnectionString("connectionString"))
                .EnableSensitiveDataLogging()
        );
        
        //add jwt settings
        services.AddJwtConfigurations(configuration);
        
        //add cloudinary settings
        services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
        
        // add quartz
        
        // services.AddQuartz(options =>
        // {
        //     options.UseMicrosoftDependencyInjectionJobFactory();
        // });
        //
        // services.AddQuartzHostedService(options =>
        // {
        //     options.WaitForJobsToComplete = true;
        // });
        //
        // services.ConfigureOptions<LoggingBackgroundJobSetup>();

        //add services
        services.AddSingleton<IPasswordHasherService, PasswordHasherService>();
        services.AddSingleton<IJwtTokenProvider, JwtTokenProvider>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IImageUploader, ImageUploader>();
        services.AddScoped<IHtmlImageProcessor, HtmlImageProcessor>();
        services.AddScoped<IContentImageProcessor, ContentImageProcessor>();
        services.AddScoped<IAnalyticsProcessor, AnalyticsProcessor>();        
        services.AddScoped<IUserContext, UserContext.UserContext>();
        
        //testing for background service
        // services.AddHostedService<ServerTimeNotifier>();
        
        return services;
    }
    
    public class MyCustomUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext context)
        {
            return context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Ensure your UserId is stored in NameIdentifier
        }
    }
}