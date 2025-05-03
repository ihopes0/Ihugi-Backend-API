using System.Net.Mime;
using Ihugi.Application.UseCases.Chats;
using Ihugi.Application.UseCases.Chats.Commands.CreateChat;
using Ihugi.Application.UseCases.Chats.Commands.DeleteChatById;
using Ihugi.Application.UseCases.Chats.Commands.UpdateChatPut;
using Ihugi.Application.UseCases.Chats.Queries.GetChatById;
using Ihugi.Application.UseCases.Chats.Queries.GetChats;
using Ihugi.Common.ErrorWork;
using Ihugi.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ihugi.Presentation.Controllers;

// TODO: XML docs
[Route("api/chats")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]
public class ChatsController : ApiController
{
    public ChatsController(ISender sender) : base(sender)
    {
    }

    /// <summary>
    /// Получить все чаты
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpGet]
    [ProducesResponseType(typeof(ChatsResponse), 200)]
    public async Task<IActionResult> GetChats(CancellationToken cancellationToken)
    {
        var query = new GetChatsQuery();

        var result = await Sender.Send(query, cancellationToken);

        return Ok(result.Value);
    }

    /// <summary>
    /// Получить чат по Id
    /// </summary>
    /// <param name="id">Идентификатор чата</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetChatById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetChatByIdQuery(id);

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    /// <summary>
    /// Создать чат
    /// </summary>
    /// <param name="request">Тело запроса</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpPost]
    [ProducesResponseType(typeof(ChatResponse), 200)]
    public async Task<IActionResult> CreateChat([FromBody] CreateChatCommand request,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request, cancellationToken);

        return Ok(result.Value);
    }

    /// <summary>
    /// Удалить чат
    /// </summary>
    /// <param name="id">Идентификатор чата</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpDelete]
    [Route("{id:guid}")]
    [ProducesResponseType(typeof(DeletedChatResponse), 200)]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteChatById(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteChatByIdCommand(id);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NoContent();
    }


    /// <summary>
    /// Обновить пользователя
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="request">Тело запроса</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpPut]
    [Route("{id:guid}")]
    [ProducesResponseType(typeof(ChatResponse), 200)]
    [ProducesResponseType(typeof(Error), 404)]
    public async Task<IActionResult> UpdateChatPut(
        Guid id,
        [FromBody] UpdateChatPutRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateChatPutCommand(id, request.Name);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }
}