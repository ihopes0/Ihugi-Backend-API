using Ihugi.Application.Abstractions;

namespace Ihugi.Application.UseCases.Chats.Commands.CreateMessage;

/// <inheritdoc/>
/// <summary>
/// Команда для создания сообщения
/// </summary>
/// <param name="ChatId">Идентификтор чата</param>
/// <param name="AuthorId">Идентификатор автора сообщения</param>
/// <param name="Content">Содержание сообщения</param>
public sealed record CreateMessageCommand(Guid ChatId, Guid AuthorId, string Content) : ICommand<MessageResponse>;