using Ihugi.Domain.DomainEvents;
using Ihugi.Domain.Entities.Chats;
using Ihugi.Domain.Primitives;

namespace Ihugi.Domain.Entities;

/// <summary>
/// Пользователь
/// </summary>
public sealed class User : AggregateRoot
{
    private readonly List<Message> _messages = new();
    private readonly List<ChatMember> _chats = new();

    /// <summary>
    /// Конструктор для EF Core
    /// </summary>
    private User()
    {
    }

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="name">Имя пользователя</param>
    /// <param name="password">Пароль пользователя</param>
    /// <param name="email">Электронная почта пользователя</param>
    private User(
        Guid id,
        string name,
        string password,
        string email
    )
        : base(id)
    {
        Name = name;
        Password = password;
        Email = email;
    }

    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; private set; }

    /// <summary>
    /// Электронная почта
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Чаты, в которых состоит пользователь
    /// </summary>
    public IReadOnlyCollection<ChatMember> Chats => _chats.AsReadOnly();

    /// <summary>
    /// Сообщения пользователя
    /// </summary>
    public IReadOnlyCollection<Message> Messages => _messages;

    /// <summary>
    /// Статичный метод для создания экземпляра User
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="name">Имя пользователя</param>
    /// <param name="password">Пароль пользователя</param>
    /// <param name="email">Электронная почта пользователя</param>
    /// <returns>Экземпляр User</returns>
    public static User Create(
        Guid id,
        string name,
        string password,
        string email
    )
    {
        var user = new User(
            id: id,
            name: name,
            password: password,
            email: email
        );

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

        return user;
    }

    /// <summary>
    /// Метод для обновления пользователя
    /// </summary>
    /// <param name="name">Имя</param>
    /// <param name="password">Пароль</param>
    /// <param name="email">Электронная почта</param>
    public void Update(string name, string password, string email)
    {
        Name = name;
        Password = password;
        Email = email;

        RaiseDomainEvent(new UserUpdatedDomainEvent(Id));
    }
}