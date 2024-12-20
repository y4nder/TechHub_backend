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
                
                options.AddPolicy(CorsConstants.AllowLocalReactApp, policy =>
                {
                    policy.WithOrigins("http://localhost:5173") // React app's origin
                        .AllowCredentials() // Allow credentials such as cookies or HTTP authentication
                        .AllowAnyHeader()  // Allow any headers
                        .AllowAnyMethod(); // Allow any methods (GET, POST, etc.)
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
        public const string AllowLocalReactApp = "AllowLocalReactApp";
    }

    public class CorsSettings
    {
        public string[] AllowedOrigins { get; set; } = Array.Empty<string>();
    }
}