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

    // TODO: Имплементировать GET chats ручку
    [HttpGet]
    public Task<IActionResult> GetChats(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    // TODO: Имплементировать GET chats/{id} ручку
    [HttpGet]
    [Route("{chatId:guid}")]
    public Task<IActionResult> GetChatById(Guid chatId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
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