using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Request;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("chats")]
public class ChatController : ControllerBase
{
    [Route("asks")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Chat(
        [FromServices] IChatService chatService,
        [FromBody] AskRequest request,
        CancellationToken cancellationToken)
    {
        var result = await chatService.Ask(request, cancellationToken);

        return result is null
            ? NotFound("It was not possible to process your input.")
            : Ok(result);
    }
}