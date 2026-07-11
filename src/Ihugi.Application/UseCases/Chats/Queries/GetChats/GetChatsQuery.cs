using Ihugi.Application.Abstractions;

namespace Ihugi.Application.UseCases.Chats.Queries.GetChats;

/// <inheritdoc/>
/// <summary>
/// Запрос для получения всех чатов
/// </summary>
/// <param name="WithMembers">Include Chat Members in response</param>
public sealed record GetChatsQuery(bool WithMembers) : IQuery<ChatsResponse>;