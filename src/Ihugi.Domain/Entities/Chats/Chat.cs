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
    private readonly List<Message> _messages = [];
    private readonly List<ChatMember> _members = [];

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
    /// <param name="creatorId">Идентификатор создателя чата</param>
    /// <param name="name">Название</param>
    private Chat(
        Guid id,
        Guid creatorId,
        string name
    )
        : base(id)
    {
        Name = name;
        AddMember(creatorId);
    }

    /// <summary>
    /// Название чата
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Сообщения, относящиеся к чату
    /// </summary>
    public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();

    /// <summary>
    /// Пользователи в чате
    /// </summary>
    public IReadOnlyCollection<ChatMember> Members => _members.AsReadOnly();

    /// <summary>
    /// Статичный метод для создания экземпляра Chat
    /// </summary>
    /// <param name="id">Идентификатор чата</param>
    /// <param name="creatorId">Идентификтор создателя чата</param>
    /// <param name="name">Название чата</param>
    /// <returns>Экземпляр Chat</returns>
    public static Chat Create(
        Guid id,
        Guid creatorId,
        string name
    )
    {
        var chat = new Chat(
            id: id,
            creatorId: creatorId,
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

    public void AddMember(Guid userId)
    {
        _members.Add(new ChatMember(userId, Id));
    }

    /// <summary>
    /// Добавить сообщение
    /// </summary>
    /// <param name="authorId">Идентификатор автора сообщения</param>
    /// <param name="content">Тело сообщения</param>
    /// <returns>Возвращает объект Common.ErrorWork.Result</returns>
    public Result<Message> AddMessage(
        Guid authorId,
        string content
    )
    {
        var messageResult = Message.Create(
            authorId,
            Id,
            content);

        if (messageResult.IsFailure)
        {
            return messageResult;
        }

        _messages.Add(messageResult.Value!);

        return Result.Success(messageResult.Value!);
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