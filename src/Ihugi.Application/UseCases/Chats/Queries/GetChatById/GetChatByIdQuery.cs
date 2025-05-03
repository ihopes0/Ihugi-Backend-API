using Ihugi.Application.Abstractions;

namespace Ihugi.Application.UseCases.Chats.Queries.GetChatById;

/// <summary>
/// Запрос для получения чата по Id
/// </summary>
/// <param name="Id">Идентификатор чата</param>
public sealed record GetChatByIdQuery(Guid Id) : IQuery<ChatResponse>;