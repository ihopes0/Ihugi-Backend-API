using System.Security.Cryptography.X509Certificates;
using Ihugi.Common.ErrorWork;
using Ihugi.Domain.Errors;
using Ihugi.Domain.Primitives;

namespace Ihugi.Domain.Entities.Chats;

// TODO: Вынести класс из директории Chats
/// <summary>
/// Чат
/// </summary>
public class Chat : AggregateRoot
{
    private readonly List<Message> _messages = new();

    /// <summary>
    /// Конструктор для EF Core
    /// </summary>
    private Chat()
    {
    }

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="name">Название</param>
    private Chat(
        Guid id,
        string name
    )
        : base(id)
    {
        Name = name;
    }

    /// <summary>
    /// Название чата
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Сообщения, относящиеся к чату
    /// </summary>
    public IReadOnlyCollection<Message> Messages => _messages;

    /// <summary>
    /// Пользователи в чате
    /// </summary>
    public IReadOnlyCollection<User> Users { get; set; }

    /// <summary>
    /// Статичный метод для создания экземпляра Chat
    /// </summary>
    /// <param name="id">Идентификатор чата</param>
    /// <param name="name">Название чата</param>
    /// <returns>Экземпляр Chat</returns>
    public static Chat Create(
        Guid id,
        string name
    )
    {
        var chat = new Chat(
            id: id,
            name: name
        );

        return chat;
    }

    /// <summary>
    /// Метод для обновления чата
    /// </summary>
    /// <param name="name">Название чата</param>
    public void Update(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Добавить сообщение
    /// </summary>
    /// <param name="authorId">Идентификатор автора сообщения</param>
    /// <param name="content">Тело сообщения</param>
    /// <returns>Возвращает объект Common.ErrorWork.Result</returns>
    public Result AddMessage(
        Guid authorId,
        string content
    )
    {
        var messageResult = Message.Create(
            Guid.NewGuid(),
            authorId,
            Id,
            content);

        if (messageResult.IsFailure)
        {
            return messageResult;
        }

        _messages.Add(messageResult.Value!);

        return Result.Success();
    }

    /// <summary>
    /// Удалить сообщение
    /// </summary>
    /// <param name="messageId">Идентификатор сообщения</param>
    /// <returns>Возвращает объект Common.ErrorWork.Result</returns>
    public Result RemoveMessage(Guid messageId)
    {
        var message = _messages.Find(m => m.Id == messageId);

        if (message is null)
        {
            return Result.Failure(DomainErrors.Message.NotFound);
        }

        _messages.Remove(message);

        return Result.Success();
    }
}