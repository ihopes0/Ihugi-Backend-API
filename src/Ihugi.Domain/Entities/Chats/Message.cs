using Ihugi.Domain.Primitives;

namespace Ihugi.Domain.Entities.Chats;

// TODO: Вынести класс из директории Chats
/// <summary>
/// Сообщение
/// </summary>
public class Message : Entity
{
    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="id">Идентификатор сообщения</param>
    /// <param name="authorId">Индентификатор автора</param>
    /// <param name="chatId">Идентификатор чата</param>
    /// <param name="content">Содержание сообщения</param>
    public Message(Guid id, Guid authorId, Guid chatId, string content)
        : base(id)
    {
        AuthorId = authorId;
        ChatId = chatId;
        Content = content;
        SentAt = DateTime.UtcNow;
    }
    
    /// <summary>
    /// Идентификатор автора сообщения
    /// </summary>
    public Guid AuthorId { get; private init; }

    /// <summary>
    /// Содержание сообщения
    /// </summary>
    public string Content { get; private init; }

    /// <summary>
    /// Время отправления сообщения
    /// </summary>
    public DateTime SentAt { get; private init; }
    
    /// <summary>
    /// Идентификатор чата
    /// </summary>
    public Guid ChatId { get; private init; }
}
