using Application.Chats.Commands.Chat;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Endpoints;

public class ChatsModule : CarterModule
{
    public ChatsModule() : base("chats")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
            pattern: "/{chatSessionId:guid}",
            handler: async (
                [FromRoute] Guid chatSessionId,
                [FromBody] ChatRequest request,
                [FromServices] ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request.GetCommand(chatSessionId), cancellationToken);

                return Results.Ok(result);
            });
    }
}