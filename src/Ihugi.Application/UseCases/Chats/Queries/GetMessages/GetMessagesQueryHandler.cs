using System.Net.Http.Headers;
using Ihugi.Application.Abstractions;
using Ihugi.Common.ErrorWork;
using Ihugi.Domain.Errors;
using Ihugi.Domain.Repositories;
using MediatR;

namespace Ihugi.Application.UseCases.Chats.Queries.GetMessages;

public sealed class GetMessagesQueryHandler(
    IChatRepository chatRepository
)
    : IQueryHandler<GetMessagesQuery, List<MessageResponse>>
{
    public async Task<Result<List<MessageResponse>>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        var chat = await chatRepository.GetByIdWithMessagesAsync(request.ChatId, cancellationToken);

        if (chat is null)
        {
            return Result.Failure<List<MessageResponse>>(DomainErrors.Chat.NotFound(request.ChatId));
        }

        var response = chat.Messages.Select(message => new MessageResponse(
            Id: message.Id,
            AuthorId: message.AuthorId,
            ChatId: message.ChatId,
            Content: message.Content
        )).ToList();

        return Result.Success(response);
    }
}
