using WebApi.Middlewares;

namespace WebApi.Extensions;

public static class MiddlewareExtensions
{
    public static IServiceCollection AddGlobalExceptionMiddleware(this IServiceCollection services)
    {
        services.AddTransient<GlobalExceptionsMiddleware>();

        return services;
    }

    public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalExceptionsMiddleware>();

        return app;
    }
}