using Ihugi.Application.Abstractions;
using Ihugi.Application.UseCases.Users.Queries.GetUserById;
using Ihugi.Common.ErrorWork;
using Ihugi.Domain.Entities.Chats;
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
        IReadOnlyList<Chat> chats;
        if (request.WithMembers)
            chats = await _chatRepository.GetAllWithMembersAsync(cancellationToken);
        else
        {
            chats = await _chatRepository.GetAllAsync(cancellationToken);
        }

        var response = new ChatsResponse(
            [.. chats.Select(c => new ChatResponse(
                c.Id,
                c.Name,
                [.. c.Members.Select(m => new ChatMemberReponse(
                    m.UserId, 
                    m.ChatId, 
                    m.JoinedAtUtc))
                ]))
            ]);

        return Result.Success(response);
    }
}