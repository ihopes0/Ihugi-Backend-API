using Ihugi.Application.UseCases.Users.Queries.GetUserById;

namespace Ihugi.Application.UseCases.Chats;

/// <summary>
/// DTO ответа данных сущности Chat
/// </summary>
/// <param name="Id">Идентификатор чата</param>
/// <param name="Name">Название чата</param>
public sealed record ChatResponse(Guid Id, string Name, IList<ChatMemberReponse>? Members = null);