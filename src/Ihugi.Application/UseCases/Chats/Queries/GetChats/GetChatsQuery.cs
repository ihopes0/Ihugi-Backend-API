using Ihugi.Application.Abstractions;

namespace Ihugi.Application.UseCases.Chats.Queries.GetChats;

public sealed record GetChatsQuery() : IQuery<ChatsResponse>;