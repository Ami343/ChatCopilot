using Application.Chats.Commands.Chat;
using Application.Services;
using Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationIoCRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ChatRequest>());
        services.AddScoped<IChatService, ChatService>();

        return services;
    }
}