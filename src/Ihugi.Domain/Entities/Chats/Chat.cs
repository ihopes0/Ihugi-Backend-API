using System.Security.Cryptography.X509Certificates;
using Ihugi.Domain.Primitives;

namespace Ihugi.Domain.Entities.Chats;

// TODO: XML docs
// TODO: Вынести класс из директории Chats
public class Chat : AggregateRoot
{
    private Chat()
    {
    }

    public Chat(
        string name,
        Guid id = new()
    )
        : base(id)
    {
        Name = name;
    }

    public string Name { get; private set; }

    public IReadOnlyCollection<Message> Messages { get; set; }

    public IReadOnlyCollection<User> Users { get; set; }
}