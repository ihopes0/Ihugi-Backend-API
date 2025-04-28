using Ihugi.Domain.Primitives;

namespace Ihugi.Domain.Entities.Chats;

// TODO: XML docs
// TODO: Вынести класс из директории Chats
public class Message : Entity
{
    public Message(Guid id, Guid authorId, Guid chatId, string content, DateTime sentAt)
        : base(id)
    {
        AuthorId = authorId;
        ChatId = chatId;
        Content = content;
        SentAt = sentAt;
    }
    
    public Guid AuthorId { get; private init; }

    public string Content { get; private init; }

    public DateTime SentAt { get; private init; }
    
    public Guid ChatId { get; private init; }
}
