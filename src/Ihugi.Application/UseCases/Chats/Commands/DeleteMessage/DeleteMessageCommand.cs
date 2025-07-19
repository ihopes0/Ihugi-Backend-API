using Ihugi.Application.Abstractions;

namespace Ihugi.Application.UseCases.Chats.Commands.DeleteMessage;

/// <inheritdoc/>
/// <summary>
/// Команда для удаления сообщения
/// </summary>
/// <param name="ChatId">Идентификтор чата</param>
/// <param name="MessageId">Идентификатор сообщения</param>
public sealed record DeleteMessageCommand(Guid ChatId, Guid MessageId) : ICommand<MessageResponse>;