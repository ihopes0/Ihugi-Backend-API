namespace Ihugi.Application.UseCases.Messages.Commands.CreateMessage;

/// <summary>
/// DTO запроса для создания нового сообщения
/// </summary>
/// <param name="ChatId">Идентификатор чата</param>
/// <param name="AuthorId">Идентификатор автора сообщения</param>
/// <param name="Content">Содержание сообщения</param>
public sealed record CreateMessageRequest(Guid ChatId, Guid AuthorId, string Content);