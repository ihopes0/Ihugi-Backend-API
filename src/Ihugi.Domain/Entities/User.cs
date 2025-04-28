using Ihugi.Domain.DomainEvents;
using Ihugi.Domain.Entities.Chats;
using Ihugi.Domain.Primitives;

namespace Ihugi.Domain.Entities;

// TODO: XML docs
public sealed class User : AggregateRoot
{
    private User()
    {
    }

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


    public string Name { get; private set; }
    public string Password { get; private set; }
    public string Email { get; private set; }
    public IReadOnlyCollection<Chat> Chats { get; set; } = [];

    public IReadOnlyCollection<Message> Messages { get; set; } = [];

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

    public void Update(string name, string password, string email)
    {
        Name = name;
        Password = password;
        Email = email;
        
        RaiseDomainEvent(new UserUpdatedDomainEvent(Id));
    }
}