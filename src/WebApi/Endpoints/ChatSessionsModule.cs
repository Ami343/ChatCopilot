using Application.Chats.Commands.Chat;
using Application.Chats.Queries.GetByChatSessionId;
using Application.ChatSessions.Commands.Create;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Endpoints;

public class ChatSessionsModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var chatSessions = app.MapGroup("/chat-sessions").WithTags("Chat-sessions");

        chatSessions.MapPost(
            pattern: "/",
            handler: async (
                [FromBody] CreateChatSessionRequest request,
                [FromServices] ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);

                return Results.Ok(result);
            });

        chatSessions.MapPost(
            pattern: "/{chatSessionId:guid}/messages",
            handler: async (
                [FromRoute] Guid chatSessionId,
                [FromBody] ChatRequest request,
                [FromServices] ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request.GetCommand(chatSessionId), cancellationToken);

                return Results.Ok(result);
            });

        chatSessions.MapGet(
            pattern: "/{chatSessionId:guid}/messages",
            handler: async (
                [FromRoute] Guid chatSessionId,
                [FromServices] ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new GetByChatSessionIdQueryParams { ChatSessionId = chatSessionId },
                    cancellationToken);

                return Results.Ok(result);
            });
    }
}