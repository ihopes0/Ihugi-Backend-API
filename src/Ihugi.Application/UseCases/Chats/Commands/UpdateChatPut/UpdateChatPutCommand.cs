using Ihugi.Application.Abstractions;

namespace Ihugi.Application.UseCases.Chats.Commands.UpdateChatPut;

/// <inheritdoc/>
/// <summary>
/// Команда для обновления чата (PUT)
/// </summary>
/// <param name="Id">Идентификатор чата</param>
/// <param name="Name">Новое название чата</param>
public sealed record UpdateChatPutCommand(Guid Id, string Name) : ICommand<ChatResponse>;