using Ihugi.Application.UseCases.Chats.Queries.GetChatById;

namespace Ihugi.Application.UseCases.Chats;

public sealed record ChatsResponse(ChatResponse[] Chats);