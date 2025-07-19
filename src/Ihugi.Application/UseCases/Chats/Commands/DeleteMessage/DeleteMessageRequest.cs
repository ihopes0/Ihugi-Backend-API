namespace Ihugi.Application.UseCases.Chats.Commands.DeleteMessage;

/// <summary>
/// DTO Тела запроса для удаления сообщения 
/// </summary>
/// <param name="MessageId">Идентификатор сообщения</param>
public sealed record DeleteMessageRequest(Guid MessageId);