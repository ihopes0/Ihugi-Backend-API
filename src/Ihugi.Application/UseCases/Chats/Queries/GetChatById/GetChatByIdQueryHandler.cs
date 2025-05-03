using Ihugi.Application.Abstractions;
using Ihugi.Common.ErrorWork;
using Ihugi.Domain.Errors;
using Ihugi.Domain.Repositories;

namespace Ihugi.Application.UseCases.Chats.Queries.GetChatById;

public sealed class GetChatByIdQueryHandler : IQueryHandler<GetChatByIdQuery, ChatResponse>
{
    private readonly IChatRepository _chatRepository;

    public GetChatByIdQueryHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<Result<ChatResponse>> Handle(GetChatByIdQuery request, CancellationToken cancellationToken)
    {
        var chat = await _chatRepository.GetByIdAsync(request.Id, cancellationToken);

        if (chat is null)
        {
            return Result.Failure<ChatResponse>(DomainErrors.Chat.NotFound);
        }

        var response = new ChatResponse(
            Id: chat.Id,
            Name: chat.Name);

        return Result.Success(response);
    }
}