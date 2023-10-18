using Application.Chats.Commands.Chat;
using Carter;
using MediatR;

namespace WebApi.Endpoints;

public class ChatsModule : CarterModule
{
    public ChatsModule() : base("chats")
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
            pattern: "/",
            handler: async (ChatRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);

                return Results.Ok(result);
            });
    }
}