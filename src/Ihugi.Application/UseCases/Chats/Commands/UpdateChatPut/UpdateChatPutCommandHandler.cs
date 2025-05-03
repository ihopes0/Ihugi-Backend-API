using Ihugi.Application.Abstractions;
using Ihugi.Common.ErrorWork;
using Ihugi.Domain.Entities.Chats;
using Ihugi.Domain.Errors;
using Ihugi.Domain.Repositories;

namespace Ihugi.Application.UseCases.Chats.Commands.UpdateChatPut;

// TODO: xml docs
internal sealed class UpdateChatPutCommandHandler : ICommandHandler<UpdateChatPutCommand, ChatResponse>
{
    private readonly IChatRepository _chatRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateChatPutCommandHandler(IChatRepository chatRepository, IUnitOfWork unitOfWork)
    {
        _chatRepository = chatRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ChatResponse>> Handle(UpdateChatPutCommand request, CancellationToken cancellationToken)
    {
        var chat = await _chatRepository.GetByIdAsync(request.Id, cancellationToken);

        if (chat is null)
        {
            return Result.Failure<ChatResponse>(DomainErrors.Chat.NotFound);
        }
        
        chat.Update(request.Name);

        _chatRepository.Update(chat);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new ChatResponse(
            chat.Id,
            chat.Name);

        return Result.Success(response);
    }
}