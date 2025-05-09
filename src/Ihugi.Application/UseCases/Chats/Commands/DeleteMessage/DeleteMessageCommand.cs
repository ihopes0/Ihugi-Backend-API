using Ihugi.Application.Abstractions;

namespace Ihugi.Application.UseCases.Chats.Commands.DeleteMessage;

public sealed record DeleteMessageCommand(Guid ChatId, Guid MessageId) : ICommand<MessageResponse>;