namespace Ihugi.Application.UseCases.Chats;

public sealed record ChatMemberReponse(Guid UserId, Guid ChatId, DateTime JoinedAtUtc);