using Ihugi.Domain.Abstractions;

namespace Ihugi.Domain.Primitives;

/// <summary>
/// Корень аггрегата
/// </summary>
public abstract class AggregateRoot : Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();
    
    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="id">Id сущности</param>
    protected AggregateRoot(Guid id)
    {
        Id = id;
    }

    /// <summary>
    /// Конструктор для EF Core
    /// </summary>
    protected AggregateRoot()
    {
    }
    
    public new Guid Id { get; private init; }

    /// <summary>
    /// Вызывает событие домена. В данной имплементации добавляет его в коллекцию сущности.
    /// </summary>
    /// <param name="domainEvent">Событие домена</param>
    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    /// <summary>
    /// Получить события домена для сущности.
    /// </summary>
    /// <returns></returns>
    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();

    /// <summary>
    /// Очистить коллекцию вызванных событий домена.
    /// </summary>
    public void ClearDomainEvents() => _domainEvents.Clear();
}