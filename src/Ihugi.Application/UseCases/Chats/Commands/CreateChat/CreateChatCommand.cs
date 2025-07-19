using Ihugi.Application.Abstractions;

namespace Ihugi.Application.UseCases.Chats.Commands.CreateChat;

/// <inheritdoc/>
/// <summary>
/// Команда для создания чата
/// </summary>
/// <param name="CreatorId">ID пользователя, который создает чат</param>
/// <param name="Name">Имя чата</param>
public sealed record CreateChatCommand(Guid CreatorId, string Name) : ICommand<ChatResponse>;