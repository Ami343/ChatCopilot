using Carter;
using WebApi.Models.Request;
using WebApi.Services.Interfaces;

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
            handler: async (AskRequest request, IChatService service, CancellationToken cancellationToken) =>
            {
                var result = await service.Ask(request, cancellationToken);

                return Results.Ok(result);
            });
    }
}