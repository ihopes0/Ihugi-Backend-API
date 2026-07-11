using System.Net.Mime;
using Ihugi.Application.UseCases.Chats;
using Ihugi.Application.UseCases.Chats.Commands.CreateChat;
using Ihugi.Application.UseCases.Chats.Commands.CreateMessage;
using Ihugi.Application.UseCases.Chats.Commands.DeleteChatById;
using Ihugi.Application.UseCases.Chats.Commands.DeleteMessage;
using Ihugi.Application.UseCases.Chats.Commands.UpdateChatPut;
using Ihugi.Application.UseCases.Chats.Queries.GetChatById;
using Ihugi.Application.UseCases.Chats.Queries.GetChats;
using Ihugi.Application.UseCases.Chats.Queries.GetMessages;
using Ihugi.Domain.Errors;
using Ihugi.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ihugi.Presentation.Controllers;

// TODO: XML docs
[Route("api/chats")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[Authorize]
public class ChatsController(ISender sender, ILogger<ChatsController> logger) : ApiController(sender)
{

    /// <summary>
    /// Получить все чаты
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpGet]
    [ProducesResponseType(typeof(ChatsResponse), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public async Task<IActionResult> GetChats([FromQuery] GetChatsQuery query, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            var apiProblem = CreateApiProblem(result.Error, 400, "Error getting chats.");
            logger.LogInformation("{@ApiProblem}", apiProblem);
            return StatusCode(apiProblem.Status!.Value, apiProblem);
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Get not deleted chat by its Id
    /// </summary>
    /// <param name="id">Chat unique ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType(typeof(ChatsResponse), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    public async Task<ActionResult<ChatResponse>> GetChatById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetChatByIdQuery(id);

        var result = await Sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            int statusCode;
            if (result.Error == DomainErrors.Chat.NotFound(id))
                statusCode = 404;
            else
                statusCode = 400;
            
            var apiProblem = CreateApiProblem(result.Error, statusCode, "Error getting chat.");
            logger.LogInformation("{@ApiProblem}", apiProblem);
            return StatusCode(apiProblem.Status!.Value, apiProblem);
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Create new chat
    /// </summary>
    /// <param name="request">Request body payload</param>
    /// <param name="cancellationToken">Cancellation token</param>
    [HttpPost]
    [ProducesResponseType(typeof(ChatResponse), 201)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public async Task<ActionResult<ChatResponse>> CreateChat(
        [FromBody] CreateChatCommand request,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request, cancellationToken);

        if (result.IsFailure)
        {
            var apiProblem = CreateApiProblem(result.Error, 400, "Error creating chat.");
            logger.LogInformation("{@ApiProblem}", apiProblem);
            return StatusCode(apiProblem.Status!.Value, apiProblem);
        }

        return Created($"api/chats/{result.Value?.Id}", result.Value);
    }

    /// <summary>
    /// Удалить чат
    /// </summary>
    /// <param name="id">Идентификатор чата</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpDelete]
    [Route("{id:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteChatById(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteChatByIdCommand(id);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure && result.Error != DomainErrors.Chat.NotFound(id))
        {
            var apiProblem = CreateApiProblem(result.Error, 400, "Error deleting chat.");
            logger.LogInformation("{@ApiProblem}", apiProblem);
            return StatusCode(apiProblem.Status!.Value, apiProblem);
        }

        return result.IsSuccess ? Ok() : NoContent();
    }


    /// <summary>
    /// Обновить чат
    /// </summary>
    /// <param name="id">Идентификатор чата</param>
    /// <param name="request">Тело запроса</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpPut]
    [Route("{id:guid}")]
    [ProducesResponseType(typeof(ChatResponse), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public async Task<IActionResult> UpdateChatPut(
        Guid id,
        [FromBody] UpdateChatPutRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateChatPutCommand(id, request.Name);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            var apiProblem = CreateApiProblem(result.Error, 400, "Error updating chat via PUT.");
            logger.LogInformation("{@ApiProblem}", apiProblem);
            return StatusCode(apiProblem.Status!.Value, apiProblem);
        }

        return Ok(result.Value);
    }

    [HttpGet("id:guid/messages")]
    [ProducesResponseType(typeof(List<MessageResponse>), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public async Task<ActionResult<List<MessageResponse>>> GetMessages(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var query = new GetMessagesQuery(id);

        var result = await Sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            var apiProblem = CreateApiProblem(result.Error, 400, "Error getting messages.");
            logger.LogInformation("{@ApiProblem}", apiProblem);
            return StatusCode(apiProblem.Status!.Value, apiProblem);
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Добавить сообщение
    /// </summary>
    /// <param name="id">Идентификатор чата</param>
    /// <param name="request">Тело запроса</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpPost]
    [Route("{id:guid}:post-message")]
    [ProducesResponseType(typeof(MessageResponse), 201)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public async Task<IActionResult> CreateMessage(
        Guid id,
        [FromBody] CreateMessageRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateMessageCommand(
            ChatId: id,
            AuthorId: request.AuthorId,
            Content: request.Content);

        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            var apiProblem = CreateApiProblem(result.Error, 400, "Error creating message.");
            logger.LogError("{@ApiProblem}", apiProblem);
            return StatusCode(apiProblem.Status!.Value, apiProblem);
        }

        return Created($"api/chats/{id}/messages", result.Value);
    }

    [HttpDelete]
    [Route("{id:guid}:delete-message")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    [ProducesResponseType(typeof(ProblemDetails), 404)]
    public async Task<IActionResult> DeleteMessage(
        Guid id,
        [FromBody] DeleteMessageRequest request,
        CancellationToken cancellationToken)
    {
        var command = new DeleteMessageCommand(
            ChatId: id,
            MessageId: request.MessageId);

        var result = await Sender.Send(command, cancellationToken);

        int statusCode;

        if (result.IsFailure && result.Error != DomainErrors.Message.NotFound(request.MessageId))
        {
            if (result.Error == DomainErrors.Chat.NotFound(id))
                statusCode = 404;
            else
                statusCode = 400;

            var apiProblem = CreateApiProblem(result.Error, statusCode, "Error deleting message.");
            
            logger.LogError("{@ApiProblem}", apiProblem);
            return StatusCode(statusCode, apiProblem);
        }

        return result.IsSuccess ? Ok() : NoContent();
    }
}