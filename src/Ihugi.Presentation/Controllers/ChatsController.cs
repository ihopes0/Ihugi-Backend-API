using System.Net.Mime;
using Ihugi.Application.UseCases.Chats;
using Ihugi.Application.UseCases.Chats.Commands.CreateChat;
using Ihugi.Application.UseCases.Chats.Queries.GetChatById;
using Ihugi.Application.UseCases.Chats.Queries.GetChats;
using Ihugi.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Uri = System.Uri;

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

        var response = await Sender.Send(query, cancellationToken);

        return Ok(response.Value);
    }
    
    /// <summary>
    /// Получить чат по Id
    /// </summary>
    /// <param name="chatId">Идентификатор чата</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpGet]
    [Route("{chatId:guid}")]
    public async Task<IActionResult> GetChatById(Guid chatId, CancellationToken cancellationToken)
    {
        var query = new GetChatByIdQuery(chatId);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }
    
    /// <summary>
    /// Создать чат
    /// </summary>
    /// <param name="request">Тело запроса</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpPost]
    [ProducesResponseType(typeof(ChatResponse), 200)]
    public async Task<IActionResult> CreateChat([FromBody] CreateChatCommand request, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request, cancellationToken);

        return Ok(result.Value);
    }
    
    // TODO: Имплементировать DELETE chats/{id} ручку
    [HttpDelete]
    [Route("{id:guid}")]
    public Task<IActionResult> DeleteChatById(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    // TODO: Имплементировать PUT chats/{id} ручку
    [HttpPut]
    [Route("{id:guid}")]
    public Task<IActionResult> UpdateChatPut(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}