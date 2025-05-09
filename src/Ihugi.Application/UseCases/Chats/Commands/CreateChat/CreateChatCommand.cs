using Ihugi.Application.Abstractions;

namespace Ihugi.Application.UseCases.Chats.Commands.CreateChat;

public sealed record CreateChatCommand(Guid CreatorId, string Name) : ICommand<ChatResponse>;