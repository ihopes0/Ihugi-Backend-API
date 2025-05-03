using Ihugi.Application.Abstractions;

namespace Ihugi.Application.UseCases.Chats.Commands.UpdateChatPut;

// TODO: xml docs
public sealed record UpdateChatPutCommand(Guid Id, string Name) : ICommand<ChatResponse>;