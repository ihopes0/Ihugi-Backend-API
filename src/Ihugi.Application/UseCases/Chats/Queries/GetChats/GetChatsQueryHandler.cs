using Ihugi.Application.Abstractions;
using Ihugi.Common.ErrorWork;
using Ihugi.Domain.Repositories;

namespace Ihugi.Application.UseCases.Chats.Queries.GetChats;

/// <inheritdoc/>
/// <summary>
/// Хэндлер запроса получения всех чатов
/// </summary>
internal sealed class GetChatsQueryHandler : IQueryHandler<GetChatsQuery, ChatsResponse>
{
    private readonly IChatRepository _chatRepository;

    /// <summary>
    /// .ctor
    /// </summary>
    public GetChatsQueryHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    /// <inheritdoc/>
    public async Task<Result<ChatsResponse>> Handle(GetChatsQuery request, CancellationToken cancellationToken)
    {
        var chats = await _chatRepository.GetAllAsync(cancellationToken);

        var response = new ChatsResponse(
            chats.Select(c => new ChatResponse(c.Id, c.Name)).ToArray());

        return Result.Success(response);
    }
}