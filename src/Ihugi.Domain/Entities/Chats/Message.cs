using Ihugi.Common.ErrorWork;
using Ihugi.Domain.Errors;
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
    private Message(Guid id, Guid authorId, Guid chatId, string content)
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
    public Guid AuthorId { get; private set; }
    
    /// <summary>
    /// Идентификатор чата
    /// </summary>
    public Guid ChatId { get; private set; }

    /// <summary>
    /// Содержание сообщения
    /// </summary>
    public string Content { get; private set; }

    /// <summary>
    /// Время отправления сообщения
    /// </summary>
    public DateTime SentAt { get; private set; }

    internal static Result<Message> Create(
        Guid id,
        Guid authorId,
        Guid chatId,
        string content)
    {
        if (content.Trim().Length < 1)
        {
            return Result.Failure<Message>(DomainErrors.Message.EmptyMessage);
        }

        var message = new Message(
            id,
            authorId,
            chatId,
            content.Trim());

        return Result.Success(message);
    }
}
