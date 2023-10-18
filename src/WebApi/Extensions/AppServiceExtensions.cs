using Application.Services;
using Application.Services.Interfaces;

namespace WebApi.Extensions;

public static class AppServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IChatService, ChatService>();

        return services;
    }   
}