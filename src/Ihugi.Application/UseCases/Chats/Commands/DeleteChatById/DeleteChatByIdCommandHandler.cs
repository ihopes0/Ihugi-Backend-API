using Ihugi.Application.Abstractions;
using Ihugi.Common.ErrorWork;
using Ihugi.Domain.Errors;
using Ihugi.Domain.Repositories;

namespace Ihugi.Application.UseCases.Chats.Commands.DeleteChatById;

/// <inheritdoc/>
/// <summary>
/// Хэндлер команды удаления чата
/// </summary>
internal sealed class DeleteChatByIdCommandHandler : ICommandHandler<DeleteChatByIdCommand, DeletedChatResponse>
{
    private const string ChatDeletedText = "Чат удален.";
    
    private readonly IChatRepository _chatRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// .ctor
    /// </summary>
    public DeleteChatByIdCommandHandler(IChatRepository chatRepository, IUnitOfWork unitOfWork)
    {
        _chatRepository = chatRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc/>
    public async Task<Result<DeletedChatResponse>> Handle(DeleteChatByIdCommand request, CancellationToken cancellationToken)
    {
        var chat = await _chatRepository.GetByIdAsync(request.Id, cancellationToken);

        if (chat is null)
        {
            return Result.Failure<DeletedChatResponse>(DomainErrors.Chat.NotFound);
        }
        
        _chatRepository.Delete(chat);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new DeletedChatResponse(ChatDeletedText));
    }
}