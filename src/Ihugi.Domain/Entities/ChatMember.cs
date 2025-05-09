namespace Ihugi.Domain.Entities;

// TODO : XML
public sealed class ChatMember
{
    public Guid UserId { get; private set; }
    public Guid ChatId { get; private set; }
    public DateTime JoinedAtUtc { get; private set; }

    private ChatMember()
    {
    }

    public ChatMember(Guid userId, Guid chatId)
    {
        UserId = userId;
        ChatId = chatId;
        JoinedAtUtc = DateTime.UtcNow;
    }
}