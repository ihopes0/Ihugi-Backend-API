using Ihugi.Domain.DomainEvents;
using MediatR;

namespace Ihugi.Application.UseCases.Users.Events;

// TODO: XML docs
internal sealed class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
{
    public Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var task = new Task(() =>
            Console.WriteLine($"Создан новый пользователь с ID {notification.UserId}")
        );
        task.Start();
        return task;
    }
}