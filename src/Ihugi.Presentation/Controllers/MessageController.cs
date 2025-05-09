using Ihugi.Application.UseCases.Messages.Commands.CreateMessage;
using Ihugi.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ihugi.Presentation.Controllers;

/// <summary>
/// Работа с сообщениями в чатах
/// </summary>
[ApiController]
[Route("api/messages")]
public class MessageController : ApiController
{
    public MessageController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateMessage(CreateMessageRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateMessageCommand(
            ChatId: request.ChatId,
            AuthorId: request.AuthorId,
            Content: request.Content);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}