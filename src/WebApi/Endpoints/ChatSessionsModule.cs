using Application.ChatSessions.Commands.Create;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Endpoints;

public class ChatSessionsModule : CarterModule
{
    public ChatSessionsModule() : base("chat-sessions")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
            pattern: "/",
            handler: async (
                [FromBody] CreateChatSessionRequest request,
                [FromServices] ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);

                return Results.Ok(result);
            });
    }
}