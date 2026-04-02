using Ihugi.Application.Abstractions;
using Ihugi.Common.ErrorWork;

namespace Ihugi.Application.UseCases.Chats.Queries.GetMessages;

public sealed record GetMessagesQuery(Guid ChatId) : IQuery<List<MessageResponse>>;

