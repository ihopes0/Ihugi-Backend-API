using Ihugi.Application.Abstractions;
using Ihugi.Common.ErrorWork;
using Ihugi.Domain.Errors;
using Ihugi.Domain.Repositories;

namespace Ihugi.Application.UseCases.Chats.Commands.DeleteChatById;

public class DeleteChatByIdCommandHandler : ICommandHandler<DeleteChatByIdCommand, DeletedChatResponse>
{
    private readonly IChatRepository _chatRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteChatByIdCommandHandler(IChatRepository chatRepository, IUnitOfWork unitOfWork)
    {
        _chatRepository = chatRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DeletedChatResponse>> Handle(DeleteChatByIdCommand request, CancellationToken cancellationToken)
    {
        var chat = await _chatRepository.GetByIdAsync(request.Id, cancellationToken);

        if (chat is null)
        {
            return Result.Failure<DeletedChatResponse>(DomainErrors.Chat.NotFound);
        }
        
        _chatRepository.Delete(chat);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new DeletedChatResponse("Чат удален."));
    }
}