using System.Text.Json.Serialization;

namespace web_api.configurations;

public static class JsonOptionsExtensions
{
    public static IMvcBuilder ConfigureJsonOptions(this IMvcBuilder builder)
    {
        // builder.AddJsonOptions(options =>
        // {
        //     options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        //     options.JsonSerializerOptions.WriteIndented = true; 
        // });
        
        return builder;
    }
}