using Ihugi.Application.Abstractions;

namespace Ihugi.Application.UseCases.Chats.Commands.DeleteChatById;

/// <inheritdoc/>
/// <summary>
/// Команда для удаления чата
/// </summary>
/// <param name="Id">Идентификатор чата</param>
public sealed record DeleteChatByIdCommand(Guid Id) : ICommand<DeletedChatResponse>;