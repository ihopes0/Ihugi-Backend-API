using Ihugi.Application.Abstractions;

namespace Ihugi.Application.UseCases.Chats.Commands.CreateChat;

public sealed record CreateChatCommand(string Name) : ICommand<ChatResponse>;