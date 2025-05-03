using Ihugi.Application.UseCases.Chats.Queries.GetChatById;
using Ihugi.Application.UseCases.Chats.Queries.GetChats;
using Ihugi.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ihugi.Presentation.Controllers;

// TODO: XML docs
[Route("api/chats")]
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
    
    // TODO: Имплементировать POST chats ручку для создания ресурса
    [HttpPost]
    public Task<IActionResult> CreateChat(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
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