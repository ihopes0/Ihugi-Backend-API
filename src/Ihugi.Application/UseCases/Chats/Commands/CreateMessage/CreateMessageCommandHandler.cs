using Ihugi.Application.Abstractions;
using Ihugi.Common.ErrorWork;
using Ihugi.Domain.Errors;
using Ihugi.Domain.Repositories;

namespace Ihugi.Application.UseCases.Chats.Commands.CreateMessage;

/// <inheritdoc/>
/// <summary>
/// Хэндлер команды создания сообщения
/// </summary>
/// <remarks>
/// .ctor
/// </remarks>
internal sealed class CreateMessageCommandHandler(
    IChatRepository chatRepository,
    IUnitOfWork unitOfWork,
    IRealTimeCommunicationService rtcService,
    IUserRepository userRepository) : ICommandHandler<CreateMessageCommand, MessageResponse>
{
    private readonly IChatRepository _chatRepository = chatRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IRealTimeCommunicationService _rtcService = rtcService;

    /// <inheritdoc/>
    public async Task<Result<MessageResponse>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var chat = await _chatRepository.GetByIdWithMembersAsync(request.ChatId, cancellationToken);

        if (chat is null)
        {
            return Result.Failure<MessageResponse>(DomainErrors.Chat.NotFound(request.ChatId));
        }

        if (!chat.Members.Any(cm => cm.UserId == request.AuthorId))
        {
            return Result.Failure<MessageResponse>(DomainErrors.Chat.UserNotMember(request.AuthorId, chat.Name));
        }

        var messageResult = chat.AddMessage(
            authorId: request.AuthorId,
            content: request.Content);

        if (messageResult.IsFailure)
        {
            return Result.Failure<MessageResponse>(messageResult.Error);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // If some DB issues
        if (messageResult.Value?.Id is null)
        {
            return Result.Failure<MessageResponse>(DomainErrors.Message.NotCreated);
        }

        var user = await _userRepository.GetByIdAsync(messageResult.Value.AuthorId, cancellationToken);

        var response = new MessageResponse(
            messageResult.Value.Id,
            messageResult.Value.ChatId,
            messageResult.Value.AuthorId,
            messageResult.Value.Content);

        // When message is created send it to SignalR chat listeners for hot reload
        // TODO: replace chat name with chat id
        // TODO: replace user name with user id
        await _rtcService.SendMessageToGroupAsync(chat.Name, user!.Name, messageResult.Value.Content);

        return Result.Success(response);
    }
}