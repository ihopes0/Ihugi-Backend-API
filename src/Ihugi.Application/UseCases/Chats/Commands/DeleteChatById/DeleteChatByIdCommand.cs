using Ihugi.Application.Abstractions;

namespace Ihugi.Application.UseCases.Chats.Commands.DeleteChatById;

public sealed record DeleteChatByIdCommand(Guid Id) : ICommand<DeletedChatResponse>;