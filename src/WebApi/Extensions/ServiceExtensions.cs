namespace WebApi.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
        if (allowedOrigins.Length > 0)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins(allowedOrigins)
                            .WithMethods("POST", "GET", "PUT", "DELETE", "PATCH")
                            .AllowAnyHeader();
                    });
            });
        }

        return services;
    }
}