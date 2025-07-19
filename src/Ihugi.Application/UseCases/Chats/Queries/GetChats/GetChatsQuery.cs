using Ihugi.Application.Abstractions;

namespace Ihugi.Application.UseCases.Chats.Queries.GetChats;

/// <inheritdoc/>
/// <summary>
/// Запрос для получения всех чатов
/// </summary>
public sealed record GetChatsQuery() : IQuery<ChatsResponse>;