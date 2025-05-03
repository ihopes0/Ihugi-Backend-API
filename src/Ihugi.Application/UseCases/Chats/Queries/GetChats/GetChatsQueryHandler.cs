using Ihugi.Application.Abstractions;
using Ihugi.Application.UseCases.Chats.Queries.GetChatById;
using Ihugi.Common.ErrorWork;
using Ihugi.Domain.Repositories;

namespace Ihugi.Application.UseCases.Chats.Queries.GetChats;

public sealed class GetChatsQueryHandler : IQueryHandler<GetChatsQuery, ChatsResponse>
{
    private readonly IChatRepository _chatRepository;

    public GetChatsQueryHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<Result<ChatsResponse>> Handle(GetChatsQuery request, CancellationToken cancellationToken)
    {
        var chats = await _chatRepository.GetAllAsync(cancellationToken);

        var response = new ChatsResponse(
            chats.Select(c => new ChatResponse(c.Id, c.Name)).ToArray());

        return Result.Success(response);
    }
}