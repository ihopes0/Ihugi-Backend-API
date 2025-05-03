using Ihugi.Application.Abstractions;
using Ihugi.Common.ErrorWork;
using Ihugi.Domain.Entities.Chats;
using Ihugi.Domain.Repositories;

namespace Ihugi.Application.UseCases.Chats.Commands.CreateChat;

public class CreateChatCommandHandler : ICommandHandler<CreateChatCommand, ChatResponse>
{
    private readonly IChatRepository _chatRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateChatCommandHandler(IChatRepository chatRepository, IUnitOfWork unitOfWork)
    {
        _chatRepository = chatRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ChatResponse>> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        var chat = new Chat(
            Guid.NewGuid(),
            request.Name);

        await _chatRepository.AddAsync(chat, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new ChatResponse(chat.Id, chat.Name);

        return Result.Success(response);
    }
}