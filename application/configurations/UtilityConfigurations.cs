using application.utilities.ImageUploads.Article;
using application.utilities.ImageUploads.Club;
using application.utilities.ImageUploads.Profile;
using Microsoft.Extensions.DependencyInjection;

namespace application.configurations;

public static class UtilityConfigurations
{
    public static IServiceCollection AddUtilityConfigurations(this IServiceCollection services)
    {
        services.AddScoped<IArticleImageService, ArticleImageService>();
        services.AddScoped<IClubImageService, ClubImageService>();
        services.AddScoped<IProfileImageService, ProfileImageService>();
        
        return services;
    }
}