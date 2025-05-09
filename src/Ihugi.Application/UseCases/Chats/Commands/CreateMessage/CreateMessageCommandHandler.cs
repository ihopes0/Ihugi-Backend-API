using Ihugi.Application.Abstractions;
using Ihugi.Common.ErrorWork;
using Ihugi.Domain.Errors;
using Ihugi.Domain.Repositories;

namespace Ihugi.Application.UseCases.Chats.Commands.CreateMessage;

internal sealed class CreateMessageCommandHandler : ICommandHandler<CreateMessageCommand, MessageResponse>
{
    private readonly IChatRepository _chatRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRealTimeCommunicationService _rtcService;

    public CreateMessageCommandHandler(IChatRepository chatRepository, IUnitOfWork unitOfWork, IRealTimeCommunicationService rtcService, IUserRepository userRepository)
    {
        _chatRepository = chatRepository;
        _unitOfWork = unitOfWork;
        _rtcService = rtcService;
        _userRepository = userRepository;
    }

    public async Task<Result<MessageResponse>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var chat = await _chatRepository.GetByIdWithMembersAsync(request.ChatId, cancellationToken);

        if (chat is null)
        {
            return Result.Failure<MessageResponse>(DomainErrors.Chat.NotFound);
        }

        if (chat.Members.All(cm => cm.UserId != request.AuthorId))
        {
            return Result.Failure<MessageResponse>(DomainErrors.Chat.UserNotMember);
        }

        var messageResult = chat.AddMessage(
            authorId: request.AuthorId,
            content: request.Content);

        if (messageResult.IsFailure)
        {
            return Result.Failure<MessageResponse>(messageResult.Error);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (messageResult.Value?.Id is null)
        {
            return Result.Failure<MessageResponse>(DomainErrors.Message.NotCreated);
        }

        var user = await _userRepository.GetByIdAsync(messageResult.Value.AuthorId, cancellationToken);

        await _rtcService.SendMessageToGroupAsync(chat.Name, user!.Name, messageResult.Value.Content);

        var response = new MessageResponse(
            messageResult.Value.Id,
            messageResult.Value.ChatId,
            messageResult.Value.AuthorId,
            messageResult.Value.Content);

        return Result.Success(response);
    }
}