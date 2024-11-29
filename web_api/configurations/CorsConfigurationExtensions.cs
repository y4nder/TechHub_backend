namespace web_api.configurations
{
    public static class CorsConfigurationExtensions
    {
        public static IServiceCollection AddCorsConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            var corsSettings = configuration.GetSection("CorsSettings").Get<CorsSettings>() ??
                               throw new NullReferenceException("CorsSettings not found");
            
            services.AddCors(options =>
            {
                // Allow all origins, methods, and headers
                options.AddPolicy(CorsConstants.AllowAll, policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });

                // Allow specific origins
                options.AddPolicy(CorsConstants.AllowSpecificOrigins, policy =>
                {
                    policy.WithOrigins(corsSettings!.AllowedOrigins)
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });

                // Allow specific headers only
                options.AddPolicy(CorsConstants.AllowSpecificHeaders, policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .WithHeaders("Authorization", "Content-Type");
                });
            });

            return services;
        }
    }

    public static class CorsConstants
    {
        public const string AllowAll = "AllowAll";
        public const string AllowSpecificOrigins = "AllowSpecificOrigins";
        public const string AllowSpecificHeaders = "AllowSpecificHeaders";
    }

    public class CorsSettings
    {
        public string[] AllowedOrigins { get; set; } = Array.Empty<string>();
    }
}