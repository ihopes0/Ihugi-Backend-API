using Ihugi.Application.Abstractions;
using Ihugi.Common.ErrorWork;
using Ihugi.Domain.Errors;
using Ihugi.Domain.Repositories;

namespace Ihugi.Application.UseCases.Chats.Commands.DeleteMessage;

/// <inheritdoc/>
/// <summary>
/// Хэндлер команды удаления сообщения
/// </summary>
internal sealed class DeleteMessageCommandHandler : ICommandHandler<DeleteMessageCommand, MessageResponse>
{
    private readonly IChatRepository _chatRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// .ctor
    /// </summary>
    public DeleteMessageCommandHandler(IChatRepository chatRepository, IUnitOfWork unitOfWork)
    {
        _chatRepository = chatRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<Result<MessageResponse>> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
    {
        var chat = await _chatRepository.GetByIdWithMessagesAsync(request.ChatId, cancellationToken);

        if (chat is null)
        {
            return Result.Failure<MessageResponse>(DomainErrors.Chat.NotFound);
        }

        var messageResult = chat.RemoveMessage(request.MessageId);

        if (messageResult.IsFailure)
        {
            return Result.Failure<MessageResponse>(messageResult.Error);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new MessageResponse(
            messageResult.Value!.Id,
            messageResult.Value.ChatId,
            messageResult.Value.AuthorId,
            messageResult.Value.Content);

        return Result.Success(response);
    }
}