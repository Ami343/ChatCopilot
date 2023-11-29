using Application.Behaviours;
using Application.Chats.Commands.Chat;
using Application.Common.Validators;
using Application.Services;
using Application.Services.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationIoCRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // CQRS
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ChatRequest>());
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        // Services
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IChatHistoryService, ChatHistoryService>();

        // Validators
        services.AddScoped<IRequestValidator<ChatCommand>, ChatCommandValidator>();

        return services;
    }
}