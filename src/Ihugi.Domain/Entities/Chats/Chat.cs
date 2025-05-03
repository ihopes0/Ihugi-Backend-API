using System.Security.Cryptography.X509Certificates;
using Ihugi.Domain.Primitives;

namespace Ihugi.Domain.Entities.Chats;

// TODO: Вынести класс из директории Chats
/// <summary>
/// Чат
/// </summary>
public class Chat : AggregateRoot
{
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
    public IReadOnlyCollection<Message> Messages { get; set; }

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
}