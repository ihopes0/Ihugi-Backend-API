namespace Ihugi.Application.UseCases.Messages;

/// <summary>
/// Ответ с информацией о сообщении
/// </summary>
/// <param name="Id">Идентификатор сообщения</param>
/// <param name="ChatId">Идентификатор чата</param>
/// <param name="AuthorId">Идентификатор автора сообщения</param>
/// <param name="Content">Содержимое сообщения</param>
public sealed record MessageResponse(Guid? Id, Guid ChatId, Guid AuthorId, string Content);