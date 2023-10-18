using Application.Chats.Commands.Chat;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationIoCRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ChatRequest>());

        return services;
    }
}